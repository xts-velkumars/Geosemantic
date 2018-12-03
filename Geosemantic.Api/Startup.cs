using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using Geosemantic.Api.Extensions;
using Geosemantic.Command.Login;
using Geosemantic.Data;
using Geosemantic.Data.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using Xen.Api.Extensions;
using Xen.Api.Infrastructure;
using Xen.Command.Extensions;
using Xen.Entity;
using Xen.Logger.Extensions;
using Xen.Oauth;
using Xen.Oauth.Extensions;
using Xen.Query.Extensions;

namespace Geosemantic.Api
{
    public class Startup
    {

        public IContainer ApplicationContainer { get; private set; }
        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            AutoMapperConfig.Setup();
        }
     
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // add configured instance of logging
            services.AddSingleton(new LoggerFactory()
                .AddConsole()
                .AddDebug()
                .AddXenLogger());


            //The order of these service registrations is critical, do not randomaly change the order without knowing what you're doing.
            services.AddOptions();

            //services.AddAspNetIdentity();
            services.ConfigureAuthorizationPolicies();

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["Oauth:SecretKey"]));
            var issuer = Configuration["Oauth:Issuer"];
            var audienceId = Configuration["Oauth:AudienceId"];

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(cfg =>
                {
                    //cfg.RequireHttpsMetadata = false;
                    //cfg.SaveToken = true;

                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        // The signing key must match!
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = signingKey,

                        // Validate the JWT Issuer (iss) claim
                        ValidateIssuer = true,
                        ValidIssuer = issuer,

                        // Validate the JWT Audience (aud) claim
                        ValidateAudience = true,
                        ValidAudience = audienceId,

                        // Validate the token expiry
                        RequireExpirationTime = true,
                        ValidateLifetime = true,

                        // If you want to allow a certain amount of clock drift, set that here:
                        ClockSkew = TimeSpan.Zero
                    };
                });

            services.AddMvc(options =>
                {
                    //By the type  
                    options.Filters.Add(typeof(XenCommandHydratorAttribute));
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(opts => opts.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver())
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<LoginCommandValidator>());

            services.AddSwaggerDocumentation(new Info
            {
                Title = "Geosemantic API",
                Version = "v1.0",
                Description = "Geosemantic",
                Contact = new Contact
                {
                    Name = "Xenovex Technologies",
                    Email = "velkumar.s@xenovex.com",
                    Url = "http://geosemantic.co.za/"
                }
            });


            services.AddAppSettings(Configuration);

            services.AddEmailSettings(Configuration);

            services.AddMediatrWithHandlers();

            ConfigureDatabase(services);

            services.ConfigureCommonServices();

            services.ConfigureDependecyInjectionForQueryPipelineBehaviours();

            services.ConfigureDependecyInjectionForCommandPipelineBehaviours();

            services.AddSingleton(Configuration);

            services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory())));

            // add logging
            services.AddLogging();

            var builder = new ContainerBuilder();

            builder.RegisterType<GeosemanticEntities>().AsSelf().As<XenContext>().InstancePerLifetimeScope();

            builder.Populate(services);
            ApplicationContainer = builder.Build();
            return new AutofacServiceProvider(ApplicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                if (!serviceScope.ServiceProvider.GetService<GeosemanticEntities>().AllMigrationsApplied())
                {
                    serviceScope.ServiceProvider.GetService<GeosemanticEntities>().Database.Migrate();
                    serviceScope.ServiceProvider.GetService<GeosemanticEntities>().EnsureSeeded();
                }
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            ConfigureAuth(app);

            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });


         //   app.UseHttpsRedirection();
            app.UseMvc();

            app.UseSwaggerDocumentation("Geosemantic API V1");
        }

        protected void ConfigureAuth(IApplicationBuilder app)
        {
            var tokenEndpoint = Configuration["Oauth:TokenEndpoint"];
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["Oauth:SecretKey"]));
            var issuer = Configuration["Oauth:Issuer"];
            var audienceId = Configuration["Oauth:AudienceId"];
            var tokenExpirationInHours = Convert.ToInt64(Configuration["Oauth:AccessTokenExpirationInHours"]);

            app.UseSimpleTokenProvider(new TokenProviderOptions
            {
                Path = tokenEndpoint,
                Audience = audienceId,
                Issuer = issuer,
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256),
                Expiration = TimeSpan.FromHours(tokenExpirationInHours),
                IdentityResolver = GetIdentity,
            });

            //Use only ONE of the below options:
            app.UseAuthentication();

        }

        private async Task<Tuple<bool, ClaimsIdentity>> GetIdentity(string username, string password, string param = "")
        {
            var mediatr = ApplicationContainer.Resolve<IMediator>();

            var result = await mediatr.Send(new LoginCommand
            {
                UserName = username,
                Password = password
            });

            var claim = new ClaimsIdentity(result.Value);

            var errorclaims = new List<Claim>
            {
                new Claim("exception", result.HasException  ? result.Exception?.Message : result.HtmlFormattedFailures )
            };

            var errorClaimsIdentity = new ClaimsIdentity(errorclaims);
            return result.IsSuccess ? Tuple.Create(true, claim) : Tuple.Create(false, errorClaimsIdentity);
        }

        protected virtual void ConfigureDatabase(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("ConnnectionString");
            services.AddDbContext<GeosemanticEntities>(options => options.UseSqlServer(connectionString));
        
        }
    }
}
