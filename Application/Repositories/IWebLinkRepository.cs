using SL2021.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SL2021.Application.Repositories
{
    public interface IWebLinkRepository : IRepository<WebLink, int>
    {
        Task<WebLink> FindByURL(
            string URL,
            CancellationToken cancellationToken);
        Task<WebLink> FindNoneIndexed(CancellationToken cancellationToken);
    }
}