using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SL2021.Application.Services.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace SL2021.API.Controllers.Image
{
    public partial class ImageController
    {
        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPaged(
            [FromQuery] GetPagedRequest request, 
            int id, 
            CancellationToken cancellationToken)
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
    }
}