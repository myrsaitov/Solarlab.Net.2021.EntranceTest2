using System;
using WidePictBoard.Domain.General;

namespace WidePictBoard.Application.Services.Category.Contracts
{
    public static class Create
    {

        public sealed class Request
        {
            public string Name { get; set; }
            public int? ParentCategoryId { get; set; }
            public CategoryStatus Status { get; set; }
            public DateTime CreatedAt { get; set; }
        }

        public sealed class Response
        {
            public int Id { get; set; }
        }
    }
}