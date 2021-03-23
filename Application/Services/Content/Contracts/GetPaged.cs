using System.Collections.Generic;
using System.Linq;

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
                public int Id { get; set; }
                public string Title { get; set; }
                public string Body { get; set; }
                public string Status { get; set; }
                public decimal Price { get; set; }
            }
        }
    }
}