using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WidePictBoard.Application.Services.Tag.Contracts;

namespace WidePictBoard.API.Controllers.Tag
{
    public partial class TagController
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(TagCreateRequest request, CancellationToken cancellationToken)
        {
            var response = await _tagService.Create(new Create.Request
            {
                Body = request.Body,
                ContentId = request.ContentId
            }, cancellationToken);

            return Created($"api/v1/tags/{response.Id}", new { });
        }
        public sealed class TagCreateRequest
        {
            [Required]
            [MaxLength(50)]
            public string Body { get; set; }
            [Required]
            [Range(1, 100_000_000_000)]
            public int ContentId { get; set; }
        }
    }
}