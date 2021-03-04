using System.Collections.Generic;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WidePictBoard.Application.User.Interface;
using WidePictBoard.API.Controllers;

namespace WidePictBoard.API.Controllers.User
{
    [Route("api/v1/users")]
    [ApiController]
    [AllowAnonymous]
    public partial class UserController : ControllerBase
    {
        public static readonly List<User> Users = new();

        public sealed class User
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public string Password { get; set; }
        }

        public sealed class UserDto
        {
            public int Id { get; set; }

            public string Name { get; set; }
        }
    }
}