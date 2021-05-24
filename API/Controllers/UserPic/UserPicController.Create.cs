using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SL2021.Application.Services.UserPic.Contracts;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace SL2021.API.Controllers.UserPic
{
    public partial class UserPicController
    {
        /*[HttpPost("contents/{id:int}")]
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

            var response = await _userPicService.Create(request, cancellationToken);

            return Ok(response);
        }*/

        [HttpPost("users/{userName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [RequestSizeLimit(long.MaxValue)]
        public async Task<ActionResult<Create.Response>> Create(
            string userName,
            [Required] IFormFile image,
            CancellationToken cancellationToken)
        {
            if (image == null)
            {
                return BadRequest("No file is uploaded.");
            }

            string baseUrl = string.Format(
                "{0}://{1}",
                HttpContext.Request.Scheme, HttpContext.Request.Host);

            var response = await _userPicService.Create(
                new Create.Request
                {
                    UserName = userName,
                    Image = image,
                    BaseURL = baseUrl
                }, 
                cancellationToken);

            return Ok(response);
        }
    }
}
