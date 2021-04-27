using System.Threading;
using System.Threading.Tasks;
using SL2021.Application.Services.Category.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace SL2021.API.Controllers.Category
{
    public partial class CategoryController
    {
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            await _categoryService.Delete(new Delete.Request
            {
                Id = id
            }, cancellationToken);
            
            return NoContent();
        }
    }
}