using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WidePictBoard.Application.Services.Category.Contracts;

namespace WidePictBoard.API.Controllers.Category
{
    public partial class CategoryController
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(CategoryCreateRequest request, CancellationToken cancellationToken)
        {
            var response = await _categoryService.Create(new Create.Request
            {
                Name = request.Name,
                ParentCategoryId = request.ParentCategoryId
            }, cancellationToken);

            return Created($"api/v1/categories/{response.Id}", new { });
        }

        public sealed class CategoryCreateRequest
        {
            [Required]
            [MaxLength(100)]
            public string Name { get; set; }

            //[Required]
            [Range(0, 100_000_000_000)]
            public int? ParentCategoryId { get; set; }
        }
    }
}