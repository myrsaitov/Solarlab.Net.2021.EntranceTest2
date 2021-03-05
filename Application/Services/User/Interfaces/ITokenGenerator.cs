
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace WidePictBoard.Application.Services.User.Interfaces
{
    public interface ITokenGenerator
    {
        Task<string> ObtainTokenFromClaims(IReadOnlyCollection<Claim> claims, CancellationToken cancellationToken);
    }
}