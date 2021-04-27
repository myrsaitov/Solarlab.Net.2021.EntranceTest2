using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SL2021.Application.Repositories
{
    public interface ITagRepository : IRepository<Domain.Tag, int>
    {
    }
}