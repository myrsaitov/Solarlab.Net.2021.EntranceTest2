using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SL2021.Application.Services.Comment.Contracts;

namespace SL2021.API.Controllers.Comment
{
    public partial class CommentController
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Authorize]
        public async Task<IActionResult> Create(
            CommentCreateRequest request, 
            CancellationToken cancellationToken)
        {
            var response = await _commentService.Create(
                new Create.Request
                {
                    Body = request.Body,
                    ContentId = request.ContentId,
                    ParentCommentId = request.ParentCommentId
                }, 
                cancellationToken);

            return Created($"api/v1/comments/{response.Id}", new { });
        }

        public sealed class CommentCreateRequest
        {
            [Required]
            [MaxLength(2048)]
            public string Body { get; set; }
            [Required]
            [Range(1, 100_000_000_000)]
            public int ContentId { get; set; }
            [Range(1, 100_000_000_000)]
            public int? ParentCommentId { get; set; }
        }
    }
}