using SL2021.Application.Repositories;
using SL2021.Domain;

namespace SL2021.Infrastructure.DataAccess.Repositories
{
    public sealed class UserPicRepository : EfRepository<UserPic, int>, IUserPicRepository
    {
        public UserPicRepository(DatabaseContext dbСontext) : base(dbСontext)
        {
        }
    }
}