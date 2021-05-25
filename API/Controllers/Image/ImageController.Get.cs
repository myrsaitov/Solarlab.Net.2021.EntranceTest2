using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using SL2021.Application.Services.Contracts;
using SL2021.Application.Services.Image.Contracts;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SL2021.API.Controllers.Image
{
    public partial class ImageController
    {

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPaged([FromQuery] GetPagedRequest request, int id, CancellationToken cancellationToken)
        {
            var result = await _imageService.GetPaged(
                a => a.ContentId == id,
                new Paged.Request
                {
                    PageSize = request.PageSize,
                    Page = request.Page
                }, 
                cancellationToken);

            return Ok(result);
        }
        [HttpGet("{id:int}/{imageName}")]
        public async Task<ActionResult> Get(
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
                contentType = "image/jpeg";
            }

            var bytes = await System.IO.File.ReadAllBytesAsync(filePath);
            return File(bytes, contentType, Path.GetFileName(filePath));
        }
    }
}
