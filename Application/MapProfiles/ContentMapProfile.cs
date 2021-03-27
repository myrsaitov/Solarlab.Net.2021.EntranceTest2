using Mapster;

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
                .Map(dest => dest.OwnerId, src => src.OwnerId)
                .Map(dest => dest.CreatedAt, src => src.CreatedAt);

            config.NewConfig<Services.Content.Contracts.GetById.Response, Domain.Content>()
                .Map(dest => dest.Title, src => src.Title)
                .Map(dest => dest.Body, src => src.Body)
                .Map(dest => dest.Price, src => src.Price)
                .Map(dest => dest.CategoryId, src => src.CategoryId)
                .Map(dest => dest.OwnerId, src => src.OwnerId)
                .Map(dest => dest.Owner, src => src.Owner)
                .Map(dest => dest.CreatedAt, src => src.CreatedAt)
                .Map(dest => dest.IsDeleted, src => src.IsDeleted);

            return config;
        }
    }
}