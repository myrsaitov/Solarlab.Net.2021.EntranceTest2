using System;

namespace WidePictBoard.Application.Services.Category.Contracts
{
    public static class GetAll
    {
        public sealed class Response : Paged.Response<Response.CategoryResponse>
        {
            public sealed class CategoryResponse
            {
                public int Id { get; set; }
                public string Name { get; set; }
                public int? ParentCategoryId { get; set; }
                public DateTime CreatedAt { get; set; }
            }
        }
    }
}