using System;
using System.Collections.Generic;

namespace WidePictBoard.Application.Services.Content.Contracts
{
    public static class Create
    {
        public sealed class Request
        {
            public string Title { get; set; }
            public string Body { get; set; }
            public decimal Price { get; set; }
            public int? CategoryId { get; set; }
            public DateTime CreatedAt { get; set; }
            public string OwnerId { get; set; }
            public bool IsDeleted { get; set; }

            public string[] Tags { get; set; }
        }
        public sealed class Response
        {
            public int Id { get; set; }
        }
    }
}