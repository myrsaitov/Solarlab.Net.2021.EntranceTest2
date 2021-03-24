using Mapster;
using WidePictBoard.Application.Common;
using WidePictBoard.Application.Identity.Contracts;
using WidePictBoard.Application.Services.User.Contracts;
using MapsterMapper;


namespace WidePictBoard.Application.MapProfiles
{
    public static class CategoryMapProfile
    {
        public static TypeAdapterConfig GetConfiguredMappingConfig()
        {
            var config = new TypeAdapterConfig();

            //public TypeAdapterSetter<TSource, TDestination> NewConfig<TSource, TDestination>();

            //TSource
            //WidePictBoard.Application.Services.Category.Contracts.Create.Request

            //TDestination
            //WidePictBoard.Domain.Category
            //Name = request.Name,
            //Status = Domain.General.CategoryStatus.InUse,
            //ParentCategoryId = request.ParentCategoryId,
            //CreatedAt = DateTime.UtcNow


            config.NewConfig<Services.Category.Contracts.Create.Request, WidePictBoard.Domain.Category>()
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Status, src => src.Status)
                .Map(dest => dest.ParentCategoryId, src => src.ParentCategoryId)
                .Map(dest => dest.CreatedAt, src => src.CreatedAt);



            config.NewConfig<Services.Category.Contracts.GetAll.Response.CategoryResponse, Domain.Category>()
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Status, src => src.Status)
                .Map(dest => dest.ParentCategoryId, src => src.ParentCategoryId)
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