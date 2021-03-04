using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace WidePictBoard.Application.User.Interface
{
    public interface IClaimsAccessor
    {
        Task<IEnumerable<Claim>> GetCurrentClaims(CancellationToken cancellationToken);
    }
}