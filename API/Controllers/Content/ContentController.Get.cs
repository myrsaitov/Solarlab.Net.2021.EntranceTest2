using System.Threading;
using System.Threading.Tasks;
using SL2021.Application.Services.Content.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SL2021.Application.Services.Contracts;

namespace SL2021.API.Controllers.Content
{
    public partial class ContentController
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetPaged([FromQuery] GetPagedTaggedRequest request, CancellationToken cancellationToken)
        {
            Paged.Response<GetPaged.Response> result;

            if (request.Tag is null)
            {
                result = await _contentService.GetPaged(new Paged.Request
                {
                    PageSize = request.PageSize,
                    Page = request.Page
                }, cancellationToken);
            }
            else
            {
                result = await _contentService.GetPaged(request.Tag, new Paged.Request
                {
                    PageSize = request.PageSize,
                    Page = request.Page
                }, cancellationToken);
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var found = await _contentService.GetById(new GetById.Request
            {
                Id = id
            }, cancellationToken);

            return Ok(found);
        }
    }
}