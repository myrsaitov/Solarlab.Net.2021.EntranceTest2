using System.Threading;
using System.Threading.Tasks;
using WidePictBoard.Application.Services.User.Contracts;

namespace WidePictBoard.Application.Services.User.Interfaces
{
    public interface IUserService
    {
        Task<Domain.User> GetCurrent(CancellationToken cancellationToken);

        Task<Register.Response> Register(Register.Request registerRequest, CancellationToken cancellationToken);
        Task<Login.Response> Login(Login.Request loginRequest, CancellationToken cancellationToken);
    }
}