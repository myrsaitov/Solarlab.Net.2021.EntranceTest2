using System.Threading;
using System.Threading.Tasks;
using SL2021.Application.Services.User.Contracts;

namespace SL2021.Application.Services.User.Interfaces
{
    public interface IUserService
    {
        Task<Register.Response> Register(Register.Request registerRequest, CancellationToken cancellationToken);
        Task Update(Update.Request request, CancellationToken cancellationToken);
    }
}