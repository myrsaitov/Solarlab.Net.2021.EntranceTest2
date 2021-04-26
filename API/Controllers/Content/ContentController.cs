using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WidePictBoard.Application.Services.Content.Interfaces;

namespace WidePictBoard.API.Controllers.Content
{
    [Route("api/v1/contents")]
    [ApiController]
    [Authorize]
    public partial class ContentController : ControllerBase
    {
        private readonly IContentService _contentService;
        public ContentController(IContentService contentService) => _contentService = contentService;
    }
}