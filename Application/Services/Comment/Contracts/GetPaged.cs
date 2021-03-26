using System;
using System.Collections.Generic;
using System.Linq;

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
            }
        }
    }
}