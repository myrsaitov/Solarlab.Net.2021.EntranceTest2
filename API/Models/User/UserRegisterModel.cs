namespace WidePictBoard.Core.Models.User
{
    public class UserRegisterModel
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public string ReturnUrl { get; set; }
        
        //Captcha token
        public string Token { get; set; }
    }
}