using Microsoft.AspNetCore.Identity;

namespace WidePictBoard.Domain.User
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserImage Avatar { get; set; }
    }
}