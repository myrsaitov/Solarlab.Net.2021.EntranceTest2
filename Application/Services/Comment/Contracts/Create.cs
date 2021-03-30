using System;
using WidePictBoard.Domain.General;

namespace WidePictBoard.Application.Services.Comment.Contracts
{
    public static class Create
    {
        public sealed class Request
        {
            public string Body { get; set; }
            public DateTime CommentDate { get; set; }
            public int ContentId { get; set; }
            public int? ParentCommentId { get; set; }
        }

        public sealed class Response
        {
            public int Id { get; set; }
        }
    }
}