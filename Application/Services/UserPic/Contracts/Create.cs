using Microsoft.AspNetCore.Http;

namespace SL2021.Application.Services.UserPic.Contracts
{
    public static class Create
    {
        public sealed class Request
        {
            public string UserName { get; set; }
            public IFormFile Image { get; set; }
            public string BaseURL { get; set; }
        }
        public sealed class Response
        {
            public string FileName { get; set; }
            public long FileSize { get; set; }
        }
    }
}