using Mapster;

namespace SL2021.Application.MapProfiles
{
    public static class CategoryMapProfile
    {
        public static TypeAdapterConfig GetConfiguredMappingConfig()
        {
            var config = TypeAdapterConfig.GlobalSettings;

            config.NewConfig<Services.Category.Contracts.Create.Request, Domain.Category>()
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.ParentCategoryId, src => src.ParentCategoryId);

            config.NewConfig<Services.Category.Contracts.Update.Request, Domain.Category>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.ParentCategoryId, src => src.ParentCategoryId);

            config.NewConfig<Domain.Category, Services.Category.Contracts.GetById.Response>()
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.ParentCategoryId, src => src.ParentCategoryId)
                .Map(dest => dest.IsDeleted, src => src.IsDeleted);

            return config;
        }
    }
}