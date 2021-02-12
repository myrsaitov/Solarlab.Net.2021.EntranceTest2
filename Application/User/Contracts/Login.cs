namespace WidePictBoard.Application.User.Contracts
{
    public static class Login
    {
        public sealed class Request
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }
        
        public sealed class Response
        {
            public string UserName { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Avatar { get; set; }
            public string Email { get; set; }
        }
    }
}