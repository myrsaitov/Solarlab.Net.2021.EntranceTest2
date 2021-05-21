using Microsoft.AspNetCore.Mvc;
using SL2021.Application.Services.Content.Interfaces;
using SL2021.Application.Services.Image.Interfaces;
using SL2021.Application.Services.User.Interfaces;

namespace SL2021.API.Controllers.Image
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/v1/images")]
    public partial class ImageController : ControllerBase
    {
        private readonly IContentService _contentService;
        private readonly IImageService _imageService;
        private readonly IUserService _userService;

        public ImageController(
            IContentService contentService,
            IImageService imageService,
            IUserService userService)
        {
            _contentService = contentService;
            _imageService = imageService;
            _userService = userService;
        }
    }
}
