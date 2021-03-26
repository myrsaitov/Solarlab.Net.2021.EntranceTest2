using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WidePictBoard.Application.Services.Comment.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WidePictBoard.API.Controllers.Comment
{
    public partial class CommentController
    {
        /// <summary>
        /// Получение всех закупок
        /// </summary>
        /// <param name="request">Dto объявления</param>
        /// <param name="cancellationToken">cancellationToken</param>
        /// <returns>Коллекция закупок</returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetPaged([FromQuery] GetAllRequest request, CancellationToken cancellationToken)
        {
            var result = await _commentService.GetPaged(new GetPaged.Request
            {
                PageSize = request.PageSize,
                Page = request.Page
            }, cancellationToken);

            return Ok(result);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {

            var found = await _commentService.GetById(new GetById.Request
            {
                Id = id
            }, cancellationToken);

            return Ok(found);
        }

        public class GetAllRequest
        {
            public int PageSize { get; set; } = 20;
            public int Page { get; set; } = 0;
        }
    }
}