using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace WidePictBoard.Application.Repositories
{
    public interface ITagRepository : IRepository<Domain.Tag, int>
    {
    }
}