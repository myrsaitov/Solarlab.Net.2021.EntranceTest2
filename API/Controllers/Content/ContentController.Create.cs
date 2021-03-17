using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WidePictBoard.Application.Services.Content.Contracts;

namespace WidePictBoard.API.Controllers.Content
{
    public partial class ContentController
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(ContentCreateRequest request, CancellationToken cancellationToken)
        {
            var response = await _contentService.Create(new Create.Request
            {
                Name = request.Name,
                Price = request.Price
            }, cancellationToken);

            return Created($"api/v1/contents/{response.Id}", new { });
        }

        public sealed class ContentCreateRequest
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