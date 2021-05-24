using SL2021.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SL2021.Application.Repositories
{
    public interface IUserRepository : IRepository<User, string>
    {
        Task<User> FindByUserName(
            string userName,
            CancellationToken cancellationToken);
    }
}