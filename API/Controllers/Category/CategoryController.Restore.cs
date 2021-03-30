using System.Threading;
using System.Threading.Tasks;
using WidePictBoard.Application.Services.Category.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace WidePictBoard.API.Controllers.Category
{
    public partial class CategoryController
    {
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Restore(int id, CancellationToken cancellationToken)
        {
            await _categoryService.Restore(new Restore.Request
            {
                Id = id
            }, cancellationToken);
            
            return NoContent();
        }
    }
}