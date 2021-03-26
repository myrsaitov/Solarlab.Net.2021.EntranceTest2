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
        private readonly IPagedBase _contentService;
        public ContentController(IPagedBase contentService) => _contentService = contentService;
    }
}