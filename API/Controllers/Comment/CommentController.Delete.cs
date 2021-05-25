using System.Threading;
using System.Threading.Tasks;
using SL2021.Application.Services.Comment.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace SL2021.API.Controllers.Comment
{
    public partial class CommentController
    {
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(
            int id, 
            CancellationToken cancellationToken)
        {
            await _commentService.Delete(
                new Delete.Request
                {
                    Id = id
                }, 
                cancellationToken);
            
            return NoContent();
        }
    }
}