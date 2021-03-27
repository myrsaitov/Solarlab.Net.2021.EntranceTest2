using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WidePictBoard.Application.Services.Content.Contracts;

namespace WidePictBoard.API.Controllers.Content
{
    public partial class ContentController
    {
        [HttpPut("update/{id:int}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Update(int id, ContentUpdateRequest request, CancellationToken cancellationToken)
        {
            var response = await _contentService.Update(new Update.Request
            {
                Id = id,
                Title = request.Title,
                Body = request.Body,
                Price = request.Price,
                CategoryId = request.CategoryId
            }, cancellationToken);

            return NoContent();
        }

        public sealed class ContentUpdateRequest
        {
            [Required]
            [MaxLength(100)]
            public string Title { get; set; }

            [Required]
            [MaxLength(1000)]
            public string Body { get; set; }

            [Required]
            [Range(0, 100_000_000_000)]
            public decimal Price { get; set; }

            [Required]
            [Range(1, 100_000_000_000)]
            public int? CategoryId { get; set; }
        }
    }
}