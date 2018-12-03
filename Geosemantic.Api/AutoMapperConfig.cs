using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ste.Domain.Entities;
using Ste.ViewModel;

namespace Ste.Api
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
