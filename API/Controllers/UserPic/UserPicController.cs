using Microsoft.AspNetCore.Mvc;
using SL2021.Application.Services.User.Interfaces;
using SL2021.Application.Services.UserPic.Interfaces;

namespace SL2021.API.Controllers.UserPic
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/v1/userpics")]
    public partial class UserPicController : ControllerBase
    {
        private readonly IUserPicService _userPicService;
        private readonly IUserService _userService;
        
        public UserPicController(
            IUserPicService userPicService,
            IUserService userService)
        {
            _userPicService = userPicService;
            _userService = userService;
        }
    }
}
