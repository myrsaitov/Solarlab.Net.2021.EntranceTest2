using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SL2021.Application.Services.Comment.Contracts;

namespace SL2021.API.Controllers.Comment
{
    public partial class CommentController
    {
        [HttpPut("update/{id:int}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Update(int id, CommentUpdateRequest request, CancellationToken cancellationToken)
        {
            var response = await _commentService.Update(new Update.Request
            {
                Id = id,
                Body = request.Body
            }, cancellationToken);

            return NoContent();
        }

        public sealed class CommentUpdateRequest
        {
            [Required]
            [MaxLength(2048)]
            public string Body { get; set; }
        }
    }
}