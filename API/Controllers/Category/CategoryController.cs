using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SL2021.Application.Services.Category.Interfaces;

namespace SL2021.API.Controllers.Category
{
    [Route("api/v1/categories")]
    [ApiController]
    [Authorize]
    public partial class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService) => _categoryService = categoryService;
    }
}