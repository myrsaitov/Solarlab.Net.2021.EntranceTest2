using System;

namespace WidePictBoard.Application.Services.Comment.Contracts
{
    public static class GetById
    {
        public sealed class Request
        {
            public int Id { get; set; }
        }

        public sealed class Response
        {
            public int? Id { get; set; }
            public string Body { get; set; }
            public DateTime CommentDate { get; set; }
            public string OwnerId { get; set; }
            public Domain.User Owner { get; set; }
            public DateTime CreatedAt { get; set; }
        }
    }
}