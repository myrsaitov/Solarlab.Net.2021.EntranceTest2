using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WidePictBoard.Application.Services.Category.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WidePictBoard.API.Controllers.Category
{
    public partial class CategoryController
    {
        /// <summary>
        /// Получение всех закупок
        /// </summary>
        /// <param name="request">Dto объявления</param>
        /// <param name="cancellationToken">cancellationToken</param>
        /// <returns>Коллекция закупок</returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll([FromQuery] GetAllRequest request, CancellationToken cancellationToken)
        {
            var result = await _categoryService.GetPaged(new GetPaged.Request
            {
                Limit = request.Limit,
                Offset = request.Offset
            }, cancellationToken);

            return Ok(result);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {

            var found = await _categoryService.GetById(new GetById.Request
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
            public int Limit { get; set; } = 20;

            /// <summary>
            /// Смещение начиная с котрого возвращаются объявления
            /// </summary>
            public int Offset { get; set; } = 0;
        }
    }
}