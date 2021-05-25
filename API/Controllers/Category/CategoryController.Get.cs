using System.Threading;
using System.Threading.Tasks;
using SL2021.Application.Services.Category.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SL2021.Application.Services.Contracts;

namespace SL2021.API.Controllers.Category
{
    public partial class CategoryController
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetPaged(
            [FromQuery] GetPagedRequest request,
            CancellationToken cancellationToken)
        {
            var result = await _categoryService.GetPaged(
                new Paged.Request
                {
                    PageSize = request.PageSize,
                    Page = request.Page
                }, 
                cancellationToken); ; // TODO что тут пропущено?

            return Ok(result);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(
            int id, 
            CancellationToken cancellationToken)
        {
            var found = await _categoryService.GetById(
                new GetById.Request
                {
                    Id = id
                }, 
                cancellationToken);

            return Ok(found);
        }
    }
}