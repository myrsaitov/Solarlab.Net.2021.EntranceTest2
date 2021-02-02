using System.Threading.Tasks;
using WidePictBoard.Domain.User;

namespace WidePictBoard.Application.UserService
{
    public interface IUserService
    {
        public Task RegisterUserAsync(User user, string password, string returnUrl);
        public Task ConfirmEmailAsync(User user, string returnUrl);
        public Task<UserDto> LoginUserAsync(User user, string password);
    }
}