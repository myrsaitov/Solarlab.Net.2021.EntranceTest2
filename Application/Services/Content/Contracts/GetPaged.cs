using System;

namespace WidePictBoard.Application.Services.Content.Contracts
{
    public static class GetPaged
    {
        public sealed class Request : Paged.Request
        {
        }

        public sealed class Response : Paged.Response<Response.ContentResponse>
        {
            public sealed class ContentResponse
            {
                public sealed class OwnerResponse
                {
                    public string Id { get; set; }
                    public string Name { get; set; }
                }

                public int Id { get; set; }
                public string Title { get; set; }
                public string Body { get; set; }
                public decimal Price { get; set; }
                public string OwnerId { get; set; }
                public OwnerResponse Owner { get; set; }
                public Domain.Category Category { get; set; }
                public DateTime CreatedAt { get; set; }
                public int? CategoryId { get; set; }
                public bool IsDeleted { get; set; }
            }
        }
    }
}