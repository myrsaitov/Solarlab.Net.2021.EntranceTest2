using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace SL2021.Application.Services.Image.Contracts
{
    public static class UploadUser
    {
        public sealed class Request
        {
            public string UserName { get; set; }
            public IFormFile Image { get; set; }
        }
        public sealed class Response : UploadImage.Response
        {
        }
    }
}