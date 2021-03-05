using System.Collections.Generic;
using System.Linq;

namespace WidePictBoard.Application.Services.Comment.Contracts
{
    public static class GetPagedComments
    {
        public sealed class Request
        {
            public int Offset { get; set; }
            public int Limit { get; set; }
        }

        public sealed class Response
        {
            public sealed class Item
            {
                public int AdId { get; set; }
                public int Id { get; set; }

                public int AuthorId { get; set; }
                public string AuthorName { get; set; }
                public string Text { get; set; }
            }

            public int Total { get; set; }
            public int Offset { get; set; }
            public int Limit { get; set; }

            public IEnumerable<Item> Items { get; set; } = Enumerable.Empty<Item>();
        }
    }
}