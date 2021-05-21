using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace SL2021.Application.Services.Image.Contracts
{
    public static class Create
    {
        public sealed class Request
        {
            public int Id { get; set; }
            public List<IFormFile> Images { get; set; }
        }
        public sealed class Response : List<UploadImageResponse>
        {
        }
    }
    public sealed class UploadImageResponse
    {
        public string FileName { get; set; }
        public long FileSize { get; set; }
    }
}