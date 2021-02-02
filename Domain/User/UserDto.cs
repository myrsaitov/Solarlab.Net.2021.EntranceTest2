using System.Collections.Generic;

namespace WidePictBoard.Domain.User
{
    public class UserDto : User
    {
        public IEnumerable<string> Roles { get; set; }
    }
}