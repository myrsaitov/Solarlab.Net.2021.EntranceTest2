using System;
using Microsoft.AspNetCore.Identity;
using WidePictBoard.Domain.User;

namespace WidePictBoard.Infrastructure
{
    public class User : IdentityUser, IUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Avatar { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}