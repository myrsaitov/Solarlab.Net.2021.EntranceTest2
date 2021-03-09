using System.Threading;
using System.Threading.Tasks;
using WidePictBoard.Application.Services.User.Contracts;

namespace WidePictBoard.Application.Services.User.Interfaces
{
    public interface IUserService
    {
        Task<Register.Response> Register(Register.Request registerRequest, CancellationToken cancellationToken);
        Task Update(Update.Request request, CancellationToken cancellationToken);
    }
}