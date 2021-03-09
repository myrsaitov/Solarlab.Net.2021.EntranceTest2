using System.Threading;
using System.Threading.Tasks;
using WidePictBoard.Application.User.Contracts;

namespace WidePictBoard.Application.User.Interface
{
    public interface IUserService
    {
        Task<Register.Response> RegisterUser(Register.Request request, string returnUrl, 
            CancellationToken token = default);
        Task ConfirmEmail(string email, string returnUrl, CancellationToken token = default);
        Task<Login.Response> LoginUser(Login.Request request, CancellationToken token = default);
    }
}