using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SL2021.Application.Services.Content.Interfaces;
using SL2021.Application.Services.Image.Interfaces;
using SL2021.Application.Services.User.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SL2021.API.Controllers.Image
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/v1/images")]
    public partial class ImageController : ControllerBase
    {
        private readonly ILogger<ImageController> _logger;
        private readonly IContentService _contentService;
        private readonly IImageService _imageService;
        private readonly IUserService _userService;

        public ImageController(
            ILogger<ImageController> logger,
            IContentService contentService,
            IImageService imageService,
            IUserService userService)
        {
            _logger = logger;
            _contentService = contentService;
            _imageService = imageService;
            _userService = userService;
        }
    }
}
