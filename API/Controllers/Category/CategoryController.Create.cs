using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SL2021.Application.Services.Category.Contracts;

namespace SL2021.API.Controllers.Category
{
    public partial class CategoryController
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Authorize]
        public async Task<IActionResult> Create(
            CategoryCreateRequest request, 
            CancellationToken cancellationToken)
        {
            var response = await _categoryService.Create(
                new Create.Request
                {
                    Name = request.Name,
                    ParentCategoryId = request.ParentCategoryId
                }, 
                cancellationToken);

            return Created($"api/v1/categories/{response.Id}", new { });
        }
        public sealed class CategoryCreateRequest
        {
            [Required]
            [MaxLength(100)]
            public string Name { get; set; }

            [Range(1, 100_000_000_000)]
            public int? ParentCategoryId { get; set; }
        }
    }
}