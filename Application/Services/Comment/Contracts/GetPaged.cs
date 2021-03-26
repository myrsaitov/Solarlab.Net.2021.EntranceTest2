using System;
using WidePictBoard.Domain.General;

namespace WidePictBoard.Application.Services.Comment.Contracts
{
    public static class GetPaged
    {
        public sealed class Request : Paged.Request
        {
        }
        public sealed class Response : Paged.Response<Response.CommentResponse>
        {
            public sealed class CommentResponse
            {
                public int Id { get; set; }
                public string Body { get; set; }
                public DateTime CommentDate { get; set; }
                public CommentStatus Status { get; set; }
                public string OwnerId { get; set; }
                public Domain.User Owner { get; set; }
                public DateTime CreatedAt { get; set; }
            }
        }
    }
}