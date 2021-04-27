using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SL2021.API.Controllers.Account
{
    public partial class AccountController
    {
        [HttpGet("confirm")]
        public async Task<IActionResult> Confirm(string userId, string token)
        {
            var isSuccessful = await _identityService.ConfirmEmail(userId, token);
            if (isSuccessful)
            {
                return Ok("Почта подтверждена");
            }

            return BadRequest("Неправильный токен или идентификатор пользователя");
        }
    }
}
