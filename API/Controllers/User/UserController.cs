using WidePictBoard.Application.Identity.Interfaces;
using WidePictBoard.Application.Services.User.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WidePictBoard.PublicApi.Controllers.User
{
    [Route("api/v1/users")]
    [ApiController]
    [AllowAnonymous]
    public partial class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IIdentityService _identityService;

        public UserController(IUserService userService, IIdentityService identityService)
        {
            _userService = userService;
            _identityService = identityService;
        }
    }
}