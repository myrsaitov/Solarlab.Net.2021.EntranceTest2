using Mapster;

namespace WidePictBoard.Application.MapProfiles
{
    public static class TagMapProfile
    {
        public static TypeAdapterConfig GetConfiguredMappingConfig()
        {
            var config = TypeAdapterConfig.GlobalSettings;

            config.NewConfig<Services.Tag.Contracts.Create.Request, Domain.Tag>()
                .Map(dest => dest.Body, src => src.Body);

            config.NewConfig<Domain.Tag, Services.Tag.Contracts.GetById.Response>()
                .Map(dest => dest.Body, src => src.Body);

            return config;
        }
    }
}