using WidePictBoard.Domain.General;

namespace WidePictBoard.Domain
{
    public class User: EntityTimed<string>
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Avatar { get; set; }
    }
}