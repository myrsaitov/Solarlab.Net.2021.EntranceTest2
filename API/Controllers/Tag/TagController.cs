using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WidePictBoard.Application.Services.Tag.Interfaces;

namespace WidePictBoard.API.Controllers.Tag
{
    [Route("api/v1/tags")]
    [ApiController]
    [Authorize]
    public partial class TagController : ControllerBase
    {
        private readonly ITagService _tagService;
        public TagController(ITagService tagService) => _tagService = tagService;
    }
}