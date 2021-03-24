using System.Collections.Generic;
using System.Linq;

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
                public string Status { get; set; }
                public int? ParentId { get; set; }
            }
        }
    }
}