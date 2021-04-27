using Mapster;

namespace SL2021.Application.MapProfiles
{
    public static class CommentMapProfile
    {
        public static TypeAdapterConfig GetConfiguredMappingConfig()
        {
            var config = TypeAdapterConfig.GlobalSettings;

            config.NewConfig<Domain.Comment, Services.Comment.Contracts.GetById.Response>()
                .Map(dest => dest.CreatedAt, src => src.CreatedAt.ToString("dd/MM/yy H:mm:ss zzz"));

            return config;
        }
    }
}