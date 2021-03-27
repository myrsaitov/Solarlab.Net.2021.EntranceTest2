using System;

namespace WidePictBoard.Application.Services.Category.Contracts
{
    public static class Update
    {
        public sealed class Request
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int? ParentCategoryId { get; set; }
        }
        public sealed class Response
        {
            public int Id { get; set; }
        }
    }
}