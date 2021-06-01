using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SL2021.Application.Services.Content.Interfaces;
using SL2021.Application.Services.WebLink.Interfaces;

namespace SL2021.API.Controllers.Parser
{
    [Route("api/v1/parser")]
    [ApiController]
    [Authorize]
    public partial class ParserController : ControllerBase
    {
        private readonly IContentService _contentService;
        private readonly IWebLinkService _webLinkService;
        public ParserController(
            IContentService contentService, 
            IWebLinkService webLinkService)
        {
            _contentService = contentService;
            _webLinkService = webLinkService;
        }
    }
}