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
        [HttpPut("update/{id:int}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Authorize]
        public async Task<IActionResult> Update(
            int id, 
            CategoryUpdateRequest request, 
            CancellationToken cancellationToken)
        {
            // TODO response
            var response = await _categoryService.Update(
                new Update.Request
                {
                    Id = id,
                    Name = request.Name,
                    ParentCategoryId = request.ParentCategoryId
                }, 
                cancellationToken);

            return NoContent();
        }

        public sealed class CategoryUpdateRequest
        {
            [Required]
            [MaxLength(100)]
            public string Name { get; set; }

            [Range(1, 100_000_000_000)]
            public int? ParentCategoryId { get; set; }
        }
    }
}