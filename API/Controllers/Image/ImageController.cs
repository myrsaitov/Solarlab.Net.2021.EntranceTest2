using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SL2021.Application.Services.Content.Interfaces;
using SL2021.Application.Services.User.Interfaces;

namespace SL2021.API.Controllers.Image
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/v1/images")]
    public partial class ImageController : ControllerBase
    {
        private readonly ILogger<ImageController> _logger;
        private readonly IContentService _contentService;
        private readonly IUserService _userService;

        public ImageController(
            ILogger<ImageController> logger,
            IContentService contentService,
            IUserService userService)
        {
            _logger = logger;
            _contentService = contentService;
            _userService = userService;
        }
    }
    public class UploadImageResponse
    {
        public string FileName { get; set; }
        public long FileSize { get; set; }
    }
}
