using System;
using WidePictBoard.Application.Services.PagedBase.Contracts;

namespace WidePictBoard.Application.Services.Comment.Contracts
{
    public static class GetPaged
    {
        public sealed class Request : Paged.Request
        {
            public int ContentId { get; set; }
        }
        
        public sealed class Response : Paged.Response<Response.SingleResponse>
        {
            public sealed class SingleResponse
            {
                public int? Id { get; set; }
                public string Body { get; set; }
                public DateTime CommentDate { get; set; }
                public string OwnerId { get; set; }
                public DateTime CreatedAt { get; set; }
                public bool IsDeleted { get; set; }
                public int ContentId { get; set; }
                public int? ParentCommentId { get; set; }
            }
        }
    }
}