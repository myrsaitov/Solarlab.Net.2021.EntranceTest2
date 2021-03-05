using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WidePictBoard.Application.Services.Content.Interfaces;
using System.Collections.Generic;
using WidePictBoard.API.Controllers.User;
using WidePictBoard.API.Controllers;


namespace WidePictBoard.API.Controllers.Content
{
    [Route("api/v1/content")]
    [ApiController]
    [Authorize]
    public partial class AdvertisementController : ControllerBase
    {
        public static readonly List<Content> Contents = new();
    }
    public sealed class Content
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public UserController.User OwnerUser { get; set; }
    }

    public sealed class ContentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int UserId { get; set; }
    }
}