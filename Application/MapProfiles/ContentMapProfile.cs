using Mapster;
using System.Linq;

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
                .Map(dest => dest.OwnerId, src => src.OwnerId);

            config.NewConfig<Services.Content.Contracts.Update.Request, Domain.Content>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Title, src => src.Title)
                .Map(dest => dest.Body, src => src.Body)
                .Map(dest => dest.Price, src => src.Price)
                .Map(dest => dest.CategoryId, src => src.CategoryId);

            config.NewConfig<Domain.Content, Services.Content.Contracts.GetById.Response>()
                .Map(dest => dest.Title, src => src.Title)
                .Map(dest => dest.Body, src => src.Body)
                .Map(dest => dest.Price, src => src.Price)
                .Map(dest => dest.CategoryId, src => src.CategoryId)
                .Map(dest => dest.OwnerId, src => src.OwnerId)
                .Map(dest => dest.Owner.FirstName, src => src.Owner.FirstName)
                .Map(dest => dest.Owner.LastName, src => src.Owner.LastName)
                .Map(dest => dest.Owner.MiddleName, src => src.Owner.MiddleName)
                .Map(dest => dest.CreatedAt, src => src.CreatedAt)
                .Map(dest => dest.IsDeleted, src => src.IsDeleted)
                .Map(dest => dest.Tags, src => src.Tags.Select(t => t.Body).ToArray());

            return config;
        }
    }
}