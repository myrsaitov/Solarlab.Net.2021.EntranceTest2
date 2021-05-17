using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace SL2021.Application.Services.Image.Contracts
{
    public static class UploadContents
    {
        public sealed class Request
        {
            public int Id { get; set; }
            public List<IFormFile> Images { get; set; }
        }
        public sealed class Response : List<UploadImage.Response>
        {
        }
    }
}