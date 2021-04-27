using System.Threading;
using System.Threading.Tasks;
using SL2021.Application.Services.Comment.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace SL2021.API.Controllers.Comment
{
    public partial class CommentController
    {
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Restore(int id, CancellationToken cancellationToken)
        {
            await _commentService.Restore(new Restore.Request
            {
                Id = id
            }, cancellationToken);
            
            return NoContent();
        }
    }
}