using Mapster;
using System.Linq;

namespace WidePictBoard.Application.MapProfiles
{
    public static class ContentMapProfile
    {
        public static TypeAdapterConfig GetConfiguredMappingConfig()
        {
            var config = TypeAdapterConfig.GlobalSettings;

            config.NewConfig<Domain.Content, Services.Content.Contracts.GetById.Response>()
                .Map(dest => dest.CreatedAt, src => src.CreatedAt.ToString("dd/MM/yy H:mm:ss zzz"))
                .Map(dest => dest.Owner.FirstName, src => src.Owner.FirstName)
                .Map(dest => dest.Owner.LastName, src => src.Owner.LastName)
                .Map(dest => dest.Owner.MiddleName, src => src.Owner.MiddleName)
                .Map(dest => dest.UserName, src => src.Owner.UserName)
                .Map(dest => dest.Tags, src => src.Tags.Select(b => b.Body).ToArray());

            config.NewConfig<Domain.Content, Services.Content.Contracts.GetPaged.Response>()
                .Map(dest => dest.CreatedAt, src => src.CreatedAt.ToString("dd/MM/yy H:mm:ss zzz"))
                .Map(dest => dest.CategoryName, src => src.Category.Name)
                .Map(dest => dest.UserName, src => src.Owner.UserName)
                .Map(dest => dest.Tags, src => src.Tags.Select(b => b.Body).ToArray());

            return config;
        }
    }
}