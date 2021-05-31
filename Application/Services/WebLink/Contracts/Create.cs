using Microsoft.AspNetCore.Http;

namespace SL2021.Application.Services.WebLink.Contracts
{
    public static class Create
    {
        public sealed class Request
        {
            public string URL { get; set; }
        }
        public sealed class Response
        {
            public int Id { get; set; }
        }
    }
}