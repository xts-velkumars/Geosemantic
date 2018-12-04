using System.Collections.Generic;
using AutoMapper;
using Geosemantic.Command.User;
using Geosemantic.Domain.Entities;
using Geosemantic.ViewModel;
using Xen.Entity.Entities;

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

                EntityToViewModelMap(cfg);
                CommandToEntityMap(cfg);

            });

            Mapper.AssertConfigurationIsValid();
        }


        private static void EntityToViewModelMap(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Role, LookUpViewModel>()
                .ForMember(dest => dest.Key, src => src.MapFrom(e => e.Id))
                .ForMember(dest => dest.Value, src => src.MapFrom(e => e.Name));


            cfg.CreateMap<Role, RolesViewModel>();

        }

        private static void CommandToEntityMap(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<SaveUserCommand, User>()
                .ForMember(dest => dest.UserSystemType, src => src.Ignore())
                .ForMember(dest => dest.Role, src => src.Ignore())
                .IgnoreXenMessageProperties();

        }

        public static IMappingExpression<TSource, TDestination> IgnoreXenMessageProperties<TSource, TDestination>(this IMappingExpression<TSource, TDestination> mapping) where TDestination : BaseEntity
        {
            return mapping
                .ForMember(dest => dest.CreationTs, src => src.Ignore())
                .ForMember(dest => dest.CreationUserId, src => src.Ignore())
                .ForMember(dest => dest.LastChangeTs, src => src.Ignore())
                .ForMember(dest => dest.LastChangeUserId, src => src.Ignore())
                .ForMember(dest => dest.StatusType, src => src.Ignore());
        }
    }
}
