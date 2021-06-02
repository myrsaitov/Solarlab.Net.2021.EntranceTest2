using System.Collections.Generic;

namespace SL2021.Application.Services.WebLink.Contracts
{
    public static class GetLinksFromPage
    {
        public sealed class Request
        {
            public string URL { get; set; }
        }
        public sealed class Response
        {
        }
    }
}