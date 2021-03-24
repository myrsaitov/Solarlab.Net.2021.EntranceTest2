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
        /// Получение всех категорий
        /// </summary>
        /// <param name="cancellationToken">cancellationToken</param>
        /// <returns>Коллекция категорий</returns>
        [HttpGet("all")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _categoryService.GetAll(cancellationToken);

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
            public int PageSize { get; set; } = 20;

            /// <summary>
            /// Смещение начиная с котрого возвращаются объявления
            /// </summary>
            public int Page { get; set; } = 0;
        }
    }
}