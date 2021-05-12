using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;

namespace SL2021.API.Controllers.Image
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/v1/images")]
    public class ImageController : ControllerBase
    {
        private readonly ILogger<ImageController> _logger;

        public ImageController(ILogger<ImageController> logger)
        {
            _logger = logger;
        }

        [HttpPost("contents/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [RequestSizeLimit(long.MaxValue)]
        public async Task<ActionResult<List<UploadImageResponse>>> ImagesUploadContents(
            int id, [Required] List<IFormFile> images)
        {
            var result = new List<UploadImageResponse>();

            if (images == null || images.Count == 0)
            {
                return BadRequest("No file is uploaded.");
            }

            foreach (var image in images)
            {
                var filePath = Path.Combine(@"Images", @"Contents", id.ToString(), image.FileName);
                new FileInfo(filePath).Directory?.Create();

                await using var stream = new FileStream(filePath, FileMode.Create);
                await image.CopyToAsync(stream);
                _logger.LogInformation($"The uploaded file [{image.FileName}] is saved as [{filePath}].");

                result.Add(new UploadImageResponse { FileName = image.FileName, FileSize = image.Length });
            }

            return Ok(result);
        }

        [HttpPost("users/{userName}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [RequestSizeLimit(long.MaxValue)]
        public async Task<ActionResult<UploadImageResponse>> ImageUploadUser(
            string userName, [Required] IFormFile image)
        {
            if (Path.GetExtension(image.FileName) != ".jpg")
            {
                return BadRequest($"The uploaded file {image.Name} is not a JPG file.");
            }

            if (image == null)
            {
                return BadRequest("No file is uploaded.");
            }

            var filePath = Path.Combine(@"Images", @"Users", $"{userName}{Path.GetExtension(image.FileName)}");
            new FileInfo(filePath).Directory?.Create();

            await using var stream = new FileStream(filePath, FileMode.Create);
            await image.CopyToAsync(stream);

            var result = new UploadImageResponse() 
            {
                FileName = image.FileName,
                FileSize = image.Length
            };

            return Ok(result);
        }



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

    public class UploadImageResponse
    {
        public string FileName { get; set; }
        public long FileSize { get; set; }
    }
}
