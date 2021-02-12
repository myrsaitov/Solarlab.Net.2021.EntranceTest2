using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WidePictBoard.Application.User.Contracts;
using WidePictBoard.Application.User.Interface;
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
            e =>
            {
                return e switch
                {
                    _ => new ObjectResult("#eS")
                    {
                       StatusCode = StatusCodes.Status500InternalServerError
                    }
                };
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
            // Captcha for later
            /*if (!await Captcha(registerModel.Token)) return BadRequest("#eRC Wrong captcha");*/
            
            return await ValidateAndRun(async () =>
            {
                await _userService.RegisterUser(registerModel.Adapt<Register.Request>(), registerModel.ReturnUrl,
                    CancellationToken.None);
                return Ok();
            });
        }

        private async Task<bool> Captcha(string token)
        {
            var captcha = await new HttpClient()
                .GetAsync(
                    "https://www.google.com/recaptcha/api/siteverify?secret=" +
                    $"{_configuration["Security:Recaptcha:Key"]}&response={token}");
            return captcha.IsSuccessStatusCode;
        }
    }
}