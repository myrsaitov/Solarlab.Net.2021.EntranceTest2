using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SL2021.Application.Services.Comment.Interfaces;

namespace SL2021.API.Controllers.Comment
{
    [Route("api/v1/comments")]
    [ApiController]
    [Authorize]
    public partial class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        public CommentController(ICommentService commentService) => _commentService = commentService;
    }
}