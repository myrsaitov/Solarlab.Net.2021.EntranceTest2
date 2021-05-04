using Mapster;
using SL2021.Application.Common;
using SL2021.Application.Identity.Contracts;
using SL2021.Application.Services.User.Contracts;
using System;

namespace SL2021.Application.MapProfiles
{
    public static class UserMapProfile
    {
        public static TypeAdapterConfig GetConfiguredMappingConfig()
        {
            var config = TypeAdapterConfig.GlobalSettings;

            config.NewConfig<CreateUser.Response, Register.Response>()
                .Map(dest => dest.UserId, src => src.UserId);

            config.NewConfig<CreateUser.Response, Domain.User>()
                .Map(dest => dest.CreatedAt, src => DateTime.UtcNow);

            config.NewConfig<Update.Request, Domain.User>()
                .Ignore(dest => dest.Id)
                .Map(dest => dest.CreatedAt, src => DateTime.UtcNow);

            config.NewConfig<Register.Request, CreateUser.Request>()
                .Map(dest => dest.UserName, src => src.UserName)
                .Map(dest => dest.Role, src => RoleConstants.UserRole);

            return config;
        }
    }
}