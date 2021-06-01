using Microsoft.EntityFrameworkCore;
using SL2021.Application.Repositories;
using SL2021.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SL2021.Infrastructure.DataAccess.Repositories
{
    public sealed class WebLinkRepository : EfRepository<WebLink, int>, IWebLinkRepository
    {
        public WebLinkRepository(DatabaseContext dbСontext) : base(dbСontext)
        {
        }
        public async Task<WebLink> FindByURL(
            string URL,
            CancellationToken cancellationToken)
        {
            return await DbСontext
                .Set<WebLink>()
                .FirstOrDefaultAsync(a => a.URL == URL, cancellationToken);
        }
    }
}