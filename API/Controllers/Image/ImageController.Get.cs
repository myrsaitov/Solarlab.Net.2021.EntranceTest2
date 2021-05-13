using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System.IO;
using System.Threading.Tasks;

namespace SL2021.API.Controllers.Image
{
    public partial class ImageController
    {
        [HttpGet("contents/{id:int}/{imageName}")]
        public async Task<ActionResult> GetImageContents(
            int id,
            string imageName)
        {
            var filePath = $"Images/Contents/{id}/{imageName}";
            if (!System.IO.File.Exists(filePath))
            {
                return BadRequest("Not found");
            }

            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filePath, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            var bytes = await System.IO.File.ReadAllBytesAsync(filePath);
            return File(bytes, contentType, Path.GetFileName(filePath));
        }


        [HttpGet("users/{userName}")]
        public async Task<ActionResult> GetImageUser(string userName)
        {
            var filePath = $"Images/Users/{userName}.jpg";
            if (!System.IO.File.Exists(filePath))
            {
                return BadRequest("Not found");
            }

            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filePath, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            var bytes = await System.IO.File.ReadAllBytesAsync(filePath);
            return File(bytes, contentType, Path.GetFileName(filePath));
        }
    }
}
