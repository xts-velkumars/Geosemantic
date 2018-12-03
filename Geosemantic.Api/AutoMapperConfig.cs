using System.Collections.Generic;
using AutoMapper;
using Geosemantic.Domain.Entities;
using Geosemantic.ViewModel;

namespace Geosemantic.Api
{
    public static class AutoMapperConfig
    {
        public static void Setup()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<KeyValuePair<int, string>, LookUpViewModel>();

                cfg.CreateMap<User, UserViewModel>();
                cfg.CreateMap<User, UsersViewModel>();
            });

            Mapper.AssertConfigurationIsValid();
        }
    }
}
