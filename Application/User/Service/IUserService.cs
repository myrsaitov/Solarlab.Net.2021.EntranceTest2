using System.Threading;
using System.Threading.Tasks;
using WidePictBoard.Application.User.Contracts;
using WidePictBoard.Domain.User;

namespace WidePictBoard.Application.User.Service
{
    public interface IUserService
    {
        public Task<Register.Response> RegisterUser(Register.Request request, string password, string returnUrl, CancellationToken token);
        public Task ConfirmEmail(UserDto user, string returnUrl, CancellationToken token);
        public Task<Login.Response> LoginUser(Login.Request request, CancellationToken token);
    }
}