using System;

namespace WidePictBoard.Domain.User
{
    /// <summary>
    /// IdentityUser abstraction
    /// </summary>
    public interface IUser
    {
        string Id { get; set; }
        string Email { get; set; }
        string UserName { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        public string PhoneNumber { get; set; }
        string Avatar { get; set; }
        DateTime CreatedOn { get; set; }
    }
}