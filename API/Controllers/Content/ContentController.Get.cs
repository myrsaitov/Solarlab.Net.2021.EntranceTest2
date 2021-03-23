using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WidePictBoard.Application.Services.Content.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WidePictBoard.API.Controllers.Content
{
    public partial class ContentController
    {
        /// <summary>
        /// Получение всех закупок
        /// </summary>
        /// <param name="request">Dto объявления</param>
        /// <param name="cancellationToken">cancellationToken</param>
        /// <returns>Коллекция закупок</returns>
        [HttpGet("paged")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPaged([FromQuery] GetAllRequest request, CancellationToken cancellationToken)
        {
            var result = await _contentService.GetPaged(new GetPaged.Request
            {
                Limit = request.PageSize,
                Offset = request.Page
            }, cancellationToken);

            return Ok(result);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {

            var found = await _contentService.GetById(new GetById.Request
            {
                Id = id
            }, cancellationToken);

            return Ok(found);
        }

        public class GetAllRequest
        {
            /// <summary>
            /// Количество возвращаемых объявлений
            /// </summary>
            public int PageSize { get; set; } = 20;

            /// <summary>
            /// Смещение начиная с котрого возвращаются объявления
            /// </summary>
            public int Page { get; set; } = 0;
        }
    }
}