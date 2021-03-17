using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WidePictBoard.Application.Services.Category.Interfaces;
using System.Collections.Generic;
using WidePictBoard.API.Controllers;


namespace WidePictBoard.API.Controllers.Category
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