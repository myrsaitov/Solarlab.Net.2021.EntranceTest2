using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SL2021.Application.Services.Image.Contracts;

namespace SL2021.API.Controllers.Image
{
    public partial class ImageController
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(ImageCreateRequest request, CancellationToken cancellationToken)
        {
            var response = await _imageService.Create(new Create.Request
            {
                Title = request.Title,
            }, cancellationToken);

            return Created($"api/v1/images/{response.Id}", new { });
        }

        public sealed class ImageCreateRequest
        {
            [Required]
            [MaxLength(100)]
            public string Title { get; set; }
        }
    }
}