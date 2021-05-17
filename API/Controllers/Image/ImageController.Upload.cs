using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SL2021.Application.Services.Image.Contracts;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace SL2021.API.Controllers.Image
{
    public partial class ImageController
    {
        [HttpPost("contents/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [RequestSizeLimit(long.MaxValue)]
        public async Task<ActionResult<UploadContents.Response>> UploadContents(
            int id,
            [Required] List<IFormFile> images,
            CancellationToken cancellationToken)
        {
            var request = new UploadContents.Request
            {
                Id = id,
                Images = new List<IFormFile>(images)
            };

            var response = await _imageService.UploadContents(request, cancellationToken);

            return Ok(response);
        }

        [HttpPost("users/{userName}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [RequestSizeLimit(long.MaxValue)]
        public async Task<ActionResult<UploadImage.Response>> UploadUser(
            string userName,
            [Required] IFormFile image,
            CancellationToken cancellationToken)
        {
            if (image == null)
            {
                return BadRequest("No file is uploaded.");
            }

            var request = new UploadUser.Request
            {
                UserName = userName,
                Image = image
            };

            var response = await _imageService.UploadUser(request, cancellationToken);

            return Ok(response);
        }
    }
}
