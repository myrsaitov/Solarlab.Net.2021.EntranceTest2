using System;

namespace WidePictBoard.Application.Services.Content.Contracts
{
    public sealed class GetPagedResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public decimal Price { get; set; }
        public string OwnerId { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? CategoryId { get; set; }
        public bool IsDeleted { get; set; }
    }
}