using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WidePictBoard.Application.Services.Comment.Interfaces;

namespace WidePictBoard.API.Controllers.Comment
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