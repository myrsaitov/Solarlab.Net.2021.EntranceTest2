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

namespace WidePictBoard.Core.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    public class UserController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        public UserController(IConfiguration configuration, IUserService userService) : base(
            async func =>
            {
                try
                {
                    return await func.Invoke();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            })
        {
            _configuration = configuration;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPut("signup")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> Register(UserRegisterModel registerModel)
        {
            return await ValidateAndRun(async () =>
            {
                await _userService.RegisterUser(registerModel.Adapt<Register.Request>(), registerModel.ReturnUrl,
                    CancellationToken.None);
                return Ok();
            });
        }
    }
}