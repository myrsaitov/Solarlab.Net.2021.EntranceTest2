using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WidePictBoard.Application.Content.Interface;


namespace WidePictBoard.PublicApi.Controllers.Content
{
    [Route("api/v1/content")]
    [ApiController]
    [Authorize]
    public partial class ContentController : BaseController
    {
        private readonly IContentService _contentService;
        
        public ContentController(IContentService contentService) => _contentService = contentService;
    }
}