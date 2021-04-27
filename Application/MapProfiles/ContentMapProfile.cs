using Mapster;
using System.Linq;

namespace SL2021.Application.MapProfiles
{
    public static class ContentMapProfile
    {
        public static TypeAdapterConfig GetConfiguredMappingConfig()
        {
            var config = TypeAdapterConfig.GlobalSettings;

            config.NewConfig<Domain.Content, Services.Content.Contracts.GetById.Response>()
                .Map(dest => dest.CreatedAt, src => src.CreatedAt.ToString("dd/MM/yy H:mm:ss zzz"))
                .Map(dest => dest.Tags, src => src.Tags.Select(b => b.Body).ToArray());

            config.NewConfig<Domain.Content, Services.Content.Contracts.GetPaged.Response>()
                .Map(dest => dest.CreatedAt, src => src.CreatedAt.ToString("dd/MM/yy H:mm:ss zzz"))
                .Map(dest => dest.CategoryName, src => src.Category.Name)
                .Map(dest => dest.Tags, src => src.Tags.Select(b => b.Body).ToArray());

            return config;
        }
    }
}