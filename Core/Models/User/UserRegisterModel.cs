namespace WidePictBoard.Models.User
{
    public class UserRegisterModel
    {
        public string Email { get; set; }
        public string Alias { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public string ReturnUrl { get; set; }
        public string Token { get; set; }
    }
}