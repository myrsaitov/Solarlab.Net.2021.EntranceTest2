using System;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WidePictBoard.Application.User.Contracts;
using WidePictBoard.Application.User.Service;
using WidePictBoard.Core.Models.User;
using WidePictBoard.Domain.User;

namespace WidePictBoard.Core.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        public UserController(IConfiguration configuration, IUserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPut("signup")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> Register(UserRegisterModel registerModel)
        {
            try
            {
                if (!registerModel.Password.Equals(registerModel.ConfirmPassword)) 
                    throw new Exception("Password must be the same");
                await _userService.RegisterUser(registerModel.Adapt<Register.Request>(), registerModel.Password, 
                    registerModel.ReturnUrl, CancellationToken.None);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
        }
    }
}