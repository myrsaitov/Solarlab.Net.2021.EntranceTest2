using System.Threading;
using System.Threading.Tasks;
using WidePictBoard.Application.User.Contracts;

namespace WidePictBoard.Application.User.Interface
{
    public interface IUserService
    {
        Task<Domain.User> GetCurrent(CancellationToken cancellationToken);

        Task<Login.Response> Login(Login.Request request, CancellationToken cancellationToken);
        Task<Register.Response> Register(Register.Request request, CancellationToken cancellationToken);
    }
}