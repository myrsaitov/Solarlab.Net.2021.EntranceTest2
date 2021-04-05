using System;
using System.Collections.Generic;

namespace WidePictBoard.Application.Services.Content.Contracts
{
    public static class GetById
    {
        public sealed class Request
        {
            public int Id { get; set; }
        }

        public sealed class Response
        {
            public sealed class OwnerResponse
            {
                public string Id { get; set; }
                public string FirstName { get; set; }
                public string LastName { get; set; }
                public string MiddleName { get; set; }
            }

            public int Id { get; set; }
            public string Title { get; set; }
            public string Body { get; set; }
            public decimal Price { get; set; }
            public string OwnerId { get; set; }
            public OwnerResponse Owner { get; set; }
            public DateTime CreatedAt { get; set; }
            public int? CategoryId { get; set; }
            public bool IsDeleted { get; set; }
        }
    }
}