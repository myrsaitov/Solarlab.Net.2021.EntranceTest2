using WidePictBoard.Application.Identity.Interfaces;
using WidePictBoard.Application.Services.User.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WidePictBoard.API.Controllers.Account
{
    [Route("api/v1/account")]
    [ApiController]
    [AllowAnonymous]
    public partial class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IIdentityService _identityService;

        public AccountController(IUserService userService, IIdentityService identityService)
        {
            _userService = userService;
            _identityService = identityService;
        }
    }
}