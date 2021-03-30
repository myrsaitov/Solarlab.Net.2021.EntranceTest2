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
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPaged([FromQuery] GetPagedRequest request, int id, CancellationToken cancellationToken)
        {
               var result = await _commentService.GetPaged(new GetPaged.Request
               {
                   ContentId = id,
                   PageSize = request.PageSize,
                   Page = request.Page
               }, cancellationToken);

               return Ok(result);
        }
    }
}