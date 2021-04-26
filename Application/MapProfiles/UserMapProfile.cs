using Mapster;
using WidePictBoard.Application.Common;
using WidePictBoard.Application.Identity.Contracts;
using WidePictBoard.Application.Services.User.Contracts;

namespace WidePictBoard.Application.MapProfiles
{
    public static class UserMapProfile
    {
        public static TypeAdapterConfig GetConfiguredMappingConfig()
        {
            var config = TypeAdapterConfig.GlobalSettings;

            config.NewConfig<CreateUser.Response, Register.Response>()
                .Map(dest => dest.UserId, src => src.UserId);

            config.NewConfig<Register.Request, CreateUser.Request>()
                .Map(dest => dest.Username, src => src.Username)
                .Map(dest => dest.Role, src => RoleConstants.UserRole);

            return config;
        }
    }
}