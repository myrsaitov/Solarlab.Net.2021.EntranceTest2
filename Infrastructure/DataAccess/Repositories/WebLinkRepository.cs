using SL2021.Application.Repositories;
using SL2021.Domain;

namespace SL2021.Infrastructure.DataAccess.Repositories
{
    public sealed class WebLinkRepository : EfRepository<WebLink, int>, IWebLinkRepository
    {
        public WebLinkRepository(DatabaseContext dbСontext) : base(dbСontext)
        {
        }
    }
}