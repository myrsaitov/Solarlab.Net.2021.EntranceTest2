using System.Threading;
using System.Threading.Tasks;
using SL2021.Application.Identity.Contracts;

namespace SL2021.Application.Identity.Interfaces
{
    public interface IIdentityService
    {
        Task<string> GetCurrentUserId(CancellationToken cancellationToken = default);
        Task<bool> IsInRole(string userId, string role, CancellationToken cancellationToken = default);
        Task<CreateUser.Response> CreateUser(CreateUser.Request request, CancellationToken cancellationToken = default);
        Task<CreateToken.Response> CreateToken(CreateToken.Request request, CancellationToken cancellationToken = default);
        Task<bool> ConfirmEmail(string userId, string token, CancellationToken cancellationToken = default);
    }
}
