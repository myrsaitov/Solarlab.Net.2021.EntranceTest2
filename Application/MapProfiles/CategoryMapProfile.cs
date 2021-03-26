using Mapster;

namespace WidePictBoard.Application.MapProfiles
{
    public static class CategoryMapProfile
    {
        public static TypeAdapterConfig GetConfiguredMappingConfig()
        {
            var config = new TypeAdapterConfig();
 
            config.NewConfig<Services.Category.Contracts.Create.Request, Domain.Category>()
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.ParentCategoryId, src => src.ParentCategoryId)
                .Map(dest => dest.CreatedAt, src => src.CreatedAt);

            config.NewConfig<Services.Category.Contracts.GetAll.Response.CategoryResponse, Domain.Category>()
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.ParentCategoryId, src => src.ParentCategoryId)
                .Map(dest => dest.CreatedAt, src => src.CreatedAt);

            return config;
        }
    }
}