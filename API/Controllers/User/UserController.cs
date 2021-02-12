using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WidePictBoard.Application.User.Interface;

namespace WidePictBoard.Core.Controllers.User
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    public partial class UserController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        public UserController(IConfiguration configuration, IUserService userService) : base(
            e =>
            {
                return e switch
                {
                    _ => new ObjectResult("#errorServer")
                    {
                       StatusCode = StatusCodes.Status500InternalServerError
                    }
                };
            })
        {
            _configuration = configuration;
            _userService = userService;
        }
    }
}