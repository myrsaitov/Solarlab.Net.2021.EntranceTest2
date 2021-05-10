using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SL2021.Application.Repositories
{
    public interface IImageRepository : IRepository<Domain.Image, int>
    {
    }
}