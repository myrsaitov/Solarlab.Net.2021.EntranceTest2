using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WidePictBoard.Application.Services.Content.Interfaces;
using System.Collections.Generic;
using WidePictBoard.API.Controllers;


namespace WidePictBoard.API.Controllers.Content
{
    [Route("api/v1/contents")]
    [ApiController]
    [Authorize]
    public partial class CommentController : ControllerBase
    {
        private readonly IContentService _contentService;

        public CommentController(IContentService contentService) => _contentService = contentService;
    }
}