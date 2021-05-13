using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;

namespace SL2021.API.Controllers.Image
{
    public partial class ImageController
    {
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

            var ImageExtensions = new List<string> { ".JPG", ".JPE", ".BMP", ".GIF", ".PNG" };

            foreach (var image in images)
            {
                if (ImageExtensions.Contains(Path.GetExtension(image.FileName).ToUpperInvariant()))
                {
                    var filePath = Path.Combine(@"Images", @"Contents", id.ToString(), image.FileName);
                    new FileInfo(filePath).Directory?.Create();

                    await using var stream = new FileStream(filePath, FileMode.Create);
                    await image.CopyToAsync(stream);
                    _logger.LogInformation($"The uploaded file [{image.FileName}] is saved as [{filePath}].");

                    result.Add(new UploadImageResponse { FileName = image.FileName, FileSize = image.Length });
                }
            }

            return Ok(result);
        }

        [HttpPost("users/{userName}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [RequestSizeLimit(long.MaxValue)]
        public async Task<ActionResult<UploadImageResponse>> ImageUploadUser(
            string userName, [Required] IFormFile image)
        {
            if (image == null)
            {
                return BadRequest("No file is uploaded.");
            }

            var ImageExtensions = new List<string> { ".JPG", ".JPE", ".BMP", ".GIF", ".PNG" };

            if (ImageExtensions.Contains(Path.GetExtension(image.FileName).ToUpperInvariant()))
            {
                var fileName = $"{userName}{Path.GetExtension(image.FileName)}";
                var filePath = Path.Combine(@"Images", @"Users", fileName);
                new FileInfo(filePath).Directory?.Create();

                await using var stream = new FileStream(filePath, FileMode.Create);
                await image.CopyToAsync(stream);

                var result = new UploadImageResponse()
                {
                    FileName = fileName,
                    FileSize = image.Length
                };

                return Ok(result);
            }
            else
            {
                return BadRequest($"The uploaded file {image.Name} is not a IMAGE file.");
            }
        }
    }
}
