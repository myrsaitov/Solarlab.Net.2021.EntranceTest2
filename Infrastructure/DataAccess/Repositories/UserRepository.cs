using SL2021.Application.Repositories;
using SL2021.Domain;

namespace SL2021.Infrastructure.DataAccess.Repositories
{
    public sealed class UserRepository : EfRepository<User, string>, IUserRepository
    {
        public UserRepository(DatabaseContext dbСontext) : base(dbСontext)
        {
        }
    }
}