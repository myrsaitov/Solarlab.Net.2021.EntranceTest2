using Microsoft.AspNetCore.Authorization;
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
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [RequestSizeLimit(long.MaxValue)]
        [Authorize]
        public async Task<ActionResult<Create.Response>> Create(
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
                    Image = image,
                    BaseURL = baseUrl
                }, 
                cancellationToken);

            return Ok(response);
        }
    }
}
