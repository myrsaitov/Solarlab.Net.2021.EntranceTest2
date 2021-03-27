using Mapster;

namespace WidePictBoard.Application.MapProfiles
{
    public static class CommentMapProfile
    {
        public static TypeAdapterConfig GetConfiguredMappingConfig()
        {
            var config = new TypeAdapterConfig();

            config.NewConfig<Services.Comment.Contracts.Create.Request, Domain.Comment>()
                .Map(dest => dest.Body, src => src.Body);
            
            config.NewConfig<Services.Comment.Contracts.GetById.Response, Domain.Comment>()
                .Map(dest => dest.Body, src => src.Body)
                .Map(dest => dest.OwnerId, src => src.OwnerId)
                .Map(dest => dest.CreatedAt, src => src.CreatedAt)
                .Map(dest => dest.IsDeleted, src => src.IsDeleted)
                .Map(dest => dest.OwnerId, src => src.OwnerId)
                .Map(dest => dest.Owner, src => src.Owner);

            return config;
        }
    }
}