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

        [HttpPut("setinuse/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> SetInUse(int id, CancellationToken cancellationToken)
        {

            await _categoryService.SetInUse(new SetInUse.Request
            {
                Id = id
            }, cancellationToken);

            return NoContent();
        }
    }
}
