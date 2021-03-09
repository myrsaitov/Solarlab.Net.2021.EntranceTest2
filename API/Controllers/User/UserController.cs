using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WidePictBoard.Application.Services.User.Interfaces;
using WidePictBoard.API.Controllers;

namespace WidePictBoard.API.Controllers.User
{
    [Route("api/v1/users")]
    [ApiController]
    [AllowAnonymous]
    public partial class UserController : ControllerBase
    {
        private readonly IUserService _userService;
    }
}