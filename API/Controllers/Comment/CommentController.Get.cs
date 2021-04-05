using System.Threading;
using System.Threading.Tasks;
using WidePictBoard.Application.Services.Comment.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WidePictBoard.Application.Services.PagedBase.Contracts;

namespace WidePictBoard.API.Controllers.Comment
{
    public partial class CommentController
    {
        [HttpGet("{contentId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPaged([FromQuery] GetPagedRequest request, int contentId, CancellationToken cancellationToken)
        {
               var result = await _commentService.GetPaged(contentId, new Paged.Request
               {
                   PageSize = request.PageSize,
                   Page = request.Page
               }, cancellationToken);

               return Ok(result);
        }
    }
}