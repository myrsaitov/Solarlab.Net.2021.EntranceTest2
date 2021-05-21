using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SL2021.Application.Services.Image.Contracts;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public async Task<ActionResult<Create.Response>> Create(
            int id,
            [Required] List<IFormFile> images,
            CancellationToken cancellationToken)
        {
            var request = new Create.Request
            {
                Id = id,
                Images = new List<IFormFile>(images)
            };

            var response = await _imageService.Create(request, cancellationToken);

            return Ok(response);
        }

        /*[HttpPost("users/{userName}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [RequestSizeLimit(long.MaxValue)]
        public async Task<ActionResult<Create.Response>> UploadUser(
            string userName,
            [Required] IFormFile image,
            CancellationToken cancellationToken)
        {
            if (image == null)
            {
                return BadRequest("No file is uploaded.");
            }

            var request = new Create.Request
            {
                UserName = userName,
                Image = image
            };

            var response = await _imageService.Create(request, cancellationToken);

            return Ok(response);
        }*/
    }
}
