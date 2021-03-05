using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using WidePictBoard.Application.Comment.Contracts;
using WidePictBoard.Application.Comment.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WidePictBoard.PublicApi.Controllers.Comment
{
    [ApiController]
    [Authorize]
    public sealed class CommentController : ControllerBase
    {
        public sealed class CreateCommentBinding
        {
            [Required]
            public int AdId { get; set; }
            
            [Required, MinLength(5), MaxLength(200)]
            public string Text { get; set; }
        }

        [HttpPost("api/v1/comments")]
        public async Task<IActionResult> CreateComment(
            [FromBody] CreateCommentBinding commentBinding,
            [FromServices] ICommentService commentService,
            CancellationToken cancellationToken
        )
        {
            var response = await commentService.CreateComment(new CreateComment.Request
            {
                Text = commentBinding.Text,
                AdId = commentBinding.AdId
            }, cancellationToken);
            
            return Created($"/api/v1/comments/{response.Id}", new { });
        }

        [HttpGet("api/v1/comments")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPaged(
            CancellationToken cancellationToken,
            [FromServices] ICommentService commentService,
            [FromQuery] int? offset = 0,
            [FromQuery] int? limit = 10
        )
        {
            return Ok(await commentService.GetPagedComments(new GetPagedComments.Request
            {
                Offset = offset.Value,
                Limit = limit.Value
            }, cancellationToken));
        }
    }
}