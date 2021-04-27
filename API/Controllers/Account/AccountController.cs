using SL2021.Application.Identity.Interfaces;
using SL2021.Application.Services.User.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SL2021.API.Controllers.Account
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