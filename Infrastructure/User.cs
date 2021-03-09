using System;
using Microsoft.AspNetCore.Identity;

namespace WidePictBoard.Infrastructure
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Avatar { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}