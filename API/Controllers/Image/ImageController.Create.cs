using Microsoft.AspNetCore.Authorization;
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
        [HttpPost("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [RequestSizeLimit(long.MaxValue)]
        [Authorize]
        public async Task<ActionResult<Create.Response>> Create(
            int id,
            [Required] List<IFormFile> images,
            CancellationToken cancellationToken)
        {
            string baseUrl = string.Format(
                "{0}://{1}",
                HttpContext.Request.Scheme, HttpContext.Request.Host);

            var response = await _imageService.Create(
                new Create.Request
                {
                    Id = id,
                    Images = new List<IFormFile>(images),
                    BaseURL = baseUrl
                }, 
                cancellationToken);

            return Ok(response);
        }
    }
}
