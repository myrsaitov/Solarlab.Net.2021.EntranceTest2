using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WidePictBoard.Models.User;

namespace WidePictBoard.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPut("signup")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> Register(UserRegisterModel registerModel)
        {
            throw new NotImplementedException();
        }
    }
}