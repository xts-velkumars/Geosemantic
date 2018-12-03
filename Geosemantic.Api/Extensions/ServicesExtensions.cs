using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Ste.Command.Login;
using Ste.Common;
using Ste.Queries.CommandAudit;
using Xen.Common.Constants;

namespace Ste.Api.Extensions
{
    public static class ServicesExtensions
    {
        public static void ConfigureAuthorizationPolicies(this IServiceCollection services)
        {
            services.AddAuthorization(opts =>
            {
                opts.AddPolicy(AuthPolicyConstants.IsAdmin, policy => policy.RequireClaim(ClaimConstants.IsAdmin));
            });
        }

        public static void AddAppSettings(this IServiceCollection services, IConfigurationRoot configuration)
        {
            var appSettingsSection = configuration.GetSection("AppSettings");
            var appSettings = new AppSettings();
            new ConfigureFromConfigurationOptions<AppSettings>(appSettingsSection).Configure(appSettings);
            services.Add(new ServiceDescriptor(typeof(AppSettings), appSettings));
        }


        public static void AddEmailSettings(this IServiceCollection services, IConfigurationRoot configuration)
        {
            var appSettingsSection = configuration.GetSection("EmailSettings");
            var emailSettings = new EmailSettings();
            new ConfigureFromConfigurationOptions<EmailSettings>(appSettingsSection).Configure(emailSettings);
            services.Add(new ServiceDescriptor(typeof(EmailSettings), emailSettings));
        }

        public static void AddMediatrWithHandlers(this IServiceCollection services)
        {
            var assembliesContainingMediatrHandlers = new[]
            {
                typeof(CommandAuditsQueryHandler),
                typeof(LoginCommandHandler)
            };
            services.AddMediatR(assembliesContainingMediatrHandlers);
        }
    }
}
