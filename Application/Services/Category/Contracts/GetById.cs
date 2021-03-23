using System.Collections.Generic;
using WidePictBoard.Domain;

namespace WidePictBoard.Application.Services.Category.Contracts
{
    public static class Pay
    {
        public sealed class Request
        {
            public int Id { get; set; }
        }
    }

    public static class GetById
    {
        public sealed class Request
        {
            public int Id { get; set; }
        }

        public sealed class Response
        {

            public int? Id { get; set; }
            public string Name { get; set; }
            public int? ParentCategoryId { get; set; }
            //public Category Category { get; set; }
        }
    }
}