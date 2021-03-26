using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WidePictBoard.Application.Services.Comment.Contracts;
using WidePictBoard.Domain.General;

namespace WidePictBoard.API.Controllers.Comment
{
    public partial class CommentController
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(CommentCreateRequest request, CancellationToken cancellationToken)
        {
            //TODO Mapster
            var response = await _commentService.Create(new Create.Request
            {
                Body = request.Body,
                CommentDate = request.CommentDate,
                Status = request.Status,
            }, cancellationToken);

            return Created($"api/v1/comments/{response.Id}", new { });
        }

        public sealed class CommentCreateRequest
        {

            [Required]
            [MaxLength(2048)]
            public string Body { get; set; }
            [Required]
            public DateTime CommentDate { get; set; }
            [Required]
            public CommentStatus Status { get; set; }
        }
    }
}