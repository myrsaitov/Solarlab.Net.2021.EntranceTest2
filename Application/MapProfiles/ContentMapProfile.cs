using Mapster;
using WidePictBoard.Application.Common;
using WidePictBoard.Application.Identity.Contracts;
using WidePictBoard.Application.Services.User.Contracts;
using MapsterMapper;


namespace WidePictBoard.Application.MapProfiles
{
    public static class ContentMapProfile
    {
        public static TypeAdapterConfig GetConfiguredMappingConfig()
        {
            var config = new TypeAdapterConfig();

            config.NewConfig<Services.Content.Contracts.Create.Request, Domain.Content>()
                .Map(dest => dest.Title, src => src.Title)
                .Map(dest => dest.Body, src => src.Body)
                .Map(dest => dest.Price, src => src.Price)
                .Map(dest => dest.CategoryId, src => src.CategoryId)
                .Map(dest => dest.Status, src => src.Status)
                .Map(dest => dest.OwnerId, src => src.OwnerId)
                .Map(dest => dest.CreatedAt, src => src.CreatedAt);

            config.NewConfig<Services.Content.Contracts.GetById.Response, Domain.Content>()
                .Map(dest => dest.Title, src => src.Title)
                .Map(dest => dest.Body, src => src.Body)
                .Map(dest => dest.Price, src => src.Price)
                .Map(dest => dest.CategoryId, src => src.CategoryId)
                .Map(dest => dest.Status, src => src.Status)
                .Map(dest => dest.OwnerId, src => src.OwnerId)
                .Map(dest => dest.CreatedAt, src => src.CreatedAt);

            config.NewConfig<Services.Content.Contracts.GetPaged.Response.ContentResponse, Domain.Content>()
                .Map(dest => dest.Title, src => src.Title)
                .Map(dest => dest.Body, src => src.Body)
                .Map(dest => dest.Price, src => src.Price)
                .Map(dest => dest.CategoryId, src => src.CategoryId)
                .Map(dest => dest.Status, src => src.Status)
                .Map(dest => dest.OwnerId, src => src.OwnerId)
                .Map(dest => dest.CreatedAt, src => src.CreatedAt);

            /* config.NewConfig<CreateUser.Response, Register.Response>()
                 .Map(dest => dest.UserId, src => src.UserId);

             config.NewConfig<Register.Request, CreateUser.Request>()
                 .Map(dest => dest.Username, src => src.Username)
                 .Map(dest => dest.Role, src => RoleConstants.UserRole);
            */



            return config;
        }
    }
}