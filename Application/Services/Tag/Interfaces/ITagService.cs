using System.Threading;
using System.Threading.Tasks;
using SL2021.Application.Services.Contracts;
using SL2021.Application.Services.Tag.Contracts;

namespace SL2021.Application.Services.Tag.Interfaces
{
    public interface ITagService
    {
        Task<Paged.Response<GetById.Response>> GetPaged(
            Paged.Request request,
            CancellationToken cancellationToken);
    }
}