using Microsoft.EntityFrameworkCore;
using SL2021.Application.Repositories;
using SL2021.Domain;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SL2021.Infrastructure.DataAccess.Repositories
{
    public sealed class UserRepository : EfRepository<User, string>, IUserRepository
    {
        public UserRepository(DatabaseContext dbСontext) : base(dbСontext)
        {
        }
        public async Task<User> FindByUserName(
            string userName, 
            CancellationToken cancellationToken)
        {
            return await DbСontext
                .Set<User>()
                .FirstOrDefaultAsync(a => a.UserName == userName, cancellationToken);
        }
    }
}