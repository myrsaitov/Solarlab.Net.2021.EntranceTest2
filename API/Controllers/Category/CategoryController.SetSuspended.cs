using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WidePictBoard.Application.Services.Category.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WidePictBoard.API.Controllers.Category
{
    public partial class CategoryController
    {

        [HttpPut("setsuspended/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> SetSuspended(int id, CancellationToken cancellationToken)
        {

            await _categoryService.SetSuspended(new SetSuspended.Request
            {
                Id = id
            }, cancellationToken);

            return NoContent();
        }
    }
}
