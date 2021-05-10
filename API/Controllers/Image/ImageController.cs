using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SL2021.Application.Services.Image.Interfaces;

namespace SL2021.API.Controllers.Image
{
    [Route("api/v1/images")]
    [ApiController]
    [Authorize]
    public partial class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;
        public ImageController(IImageService imageService) => _imageService = imageService;
    }
}