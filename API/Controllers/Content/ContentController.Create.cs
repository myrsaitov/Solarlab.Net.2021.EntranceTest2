using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SL2021.Application.Services.Content.Contracts;

namespace SL2021.API.Controllers.Content
{
    public partial class ContentController
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Authorize]
        public async Task<IActionResult> Create(
            ContentCreateRequest request, 
            CancellationToken cancellationToken)
        {
            var response = await _contentService.Create(
                new Create.Request
                {
                    Title = request.Title,
                    Body = request.Body,
                    CategoryId = request.CategoryId,
                    TagBodies = request.Tags
                }, 
                cancellationToken);

            return Created($"api/v1/contents/{response.Id}", new { });
        }

        public sealed class ContentCreateRequest
        {
            [Required]
            [MaxLength(100)]
            public string Title { get; set; }

            [Required]
            [MaxLength(1000)]
            public string Body { get; set; }

            [Required]
            [Range(1, 100_000_000_000)]
            public int CategoryId { get; set; }

            public string[] Tags { get; set; }
        }
    }
}