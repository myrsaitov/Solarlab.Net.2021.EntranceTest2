using System.Threading;
using System.Threading.Tasks;
using WidePictBoard.Application.User.Contracts;

namespace WidePictBoard.Application.User.Interfaces
{
    public interface IUserService
    {
        Task<Domain.User> GetCurrent(CancellationToken cancellationToken);

        Task<Login.Response> Login(Login.Request request, CancellationToken cancellationToken);
        Task<Register.Response> Register(Register.Request request, CancellationToken cancellationToken);
    }
}