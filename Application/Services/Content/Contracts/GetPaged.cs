using SL2021.Application.Services.Contracts;

namespace SL2021.Application.Services.Content.Contracts
{
    public static class GetPaged
    {
        public sealed class Response
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Body { get; set; }
            public decimal Price { get; set; }
            public OwnerResponse Owner { get; set; }
            public string CreatedAt { get; set; }
            public int CategoryId { get; set; }
            public string CategoryName { get; set; }
            public bool IsDeleted { get; set; }
            public string[] Tags { get; set; }
        }
    }
}