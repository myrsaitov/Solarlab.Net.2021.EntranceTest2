using System.Threading;
using System.Threading.Tasks;
using SL2021.Application.Services.Content.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SL2021.Application.Services.Contracts;
using System.Linq;

namespace SL2021.API.Controllers.Content
{
    public partial class ContentController
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetPaged([FromQuery] GetPagedContentRequest request, CancellationToken cancellationToken)
        {
            var result = new Paged.Response<GetPaged.Response>();

            if((request.SearchStr is null) && (request.UserName is null) && (request.CategoryId is null) && (request.Tag is null))
            {
                result = await _contentService.GetPaged(new Paged.Request
                {
                    PageSize = request.PageSize,
                    Page = request.Page
                }, cancellationToken);
            }
            else if ((request.SearchStr is not null) && (request.UserName is null) && (request.CategoryId is null) && (request.Tag is null))
            {
                result = await _contentService.GetPaged(
                    o => o.Body.ToLower().Contains(request.SearchStr.ToLower()) || o.Title.ToLower().Contains(request.SearchStr.ToLower()),
                    new Paged.Request
                    {
                        PageSize = request.PageSize,
                        Page = request.Page
                    }, cancellationToken);
            }
            else if ((request.SearchStr is null) && (request.UserName is not null) && (request.CategoryId is null) && (request.Tag is null))
            {
                result = await _contentService.GetPaged(
                    a => a.Owner.UserName == request.UserName,
                    new Paged.Request
                    {
                        PageSize = request.PageSize,
                        Page = request.Page
                    }, cancellationToken);
            }
            else if ((request.SearchStr is null) && (request.UserName is null) && (request.CategoryId is not null) && (request.Tag is null))
            {
                result = await _contentService.GetPaged(
                    a => a.CategoryId == request.CategoryId,
                    new Paged.Request
                    {
                        PageSize = request.PageSize,
                        Page = request.Page
                    }, cancellationToken);
            }
            else if ((request.SearchStr is null) && (request.UserName is null) && (request.CategoryId is null) && (request.Tag is not null))
            {
                result = await _contentService.GetPaged(
                    a => a.Tags.Any(t => t.Body == request.Tag),
                    new Paged.Request
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