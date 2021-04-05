using Mapster;

namespace WidePictBoard.Application.MapProfiles
{
    public static class CommentMapProfile
    {
        public static TypeAdapterConfig GetConfiguredMappingConfig()
        {
            var config = new TypeAdapterConfig();

            config.NewConfig<Services.Comment.Contracts.Create.Request, Domain.Comment>()
                .Map(dest => dest.Body, src => src.Body)
                .Map(dest => dest.ContentId, src => src.ContentId)
                .Map(dest => dest.ParentCommentId, src => src.ParentCommentId);

            config.NewConfig<Services.Comment.Contracts.Update.Request, Domain.Comment>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Body, src => src.Body);

            config.NewConfig<Domain.Comment, Services.Comment.Contracts.GetPaged.Response.SingleResponse>()
                .Map(dest => dest.Body, src => src.Body)
                .Map(dest => dest.OwnerId, src => src.OwnerId)
                .Map(dest => dest.CreatedAt, src => src.CreatedAt)
                .Map(dest => dest.IsDeleted, src => src.IsDeleted)
                .Map(dest => dest.OwnerId, src => src.OwnerId)
                .Map(dest => dest.ContentId, src => src.ContentId)
                .Map(dest => dest.ParentCommentId, src => src.ParentCommentId);

            return config;
        }
    }
}