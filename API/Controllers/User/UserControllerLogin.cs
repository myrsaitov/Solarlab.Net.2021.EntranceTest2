using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WidePictBoard.Application.User.Contracts;
using WidePictBoard.Core.Models.User;

namespace WidePictBoard.Core.Controllers.User
{
    public partial class UserController
    {
        [AllowAnonymous]
        [HttpPut("signup")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> Login(UserRegisterModel registerModel)
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