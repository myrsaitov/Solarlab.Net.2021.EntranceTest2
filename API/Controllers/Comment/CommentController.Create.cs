using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WidePictBoard.Application.Services.Comment.Contracts;

namespace WidePictBoard.API.Controllers.Comment
{
    public partial class CommentController
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(CommentCreateRequest request, CancellationToken cancellationToken)
        {
            var response = await _commentService.Create(new Create.Request
            {
                Name = request.Name,
                Price = request.Price
            }, cancellationToken);

            return Created($"api/v1/comments/{response.Id}", new { });
        }

        public sealed class CommentCreateRequest
        {
            [Required]
            [MaxLength(100)]
            public string Name { get; set; }

            [Required]
            [Range(0, 100_000_000_000)]
            public decimal Price { get; set; }
        }
    }
}