using System.Threading;
using System.Threading.Tasks;
using WidePictBoard.Application.Services.Tag.Contracts;
using WidePictBoard.Application.Services.PagedBase.Contracts;

namespace WidePictBoard.Application.Services.Tag.Interfaces
{
    public interface ITagService
    {
        Task<Create.Response> Create(Create.Request request, CancellationToken cancellationToken);
        Task<Paged.Response<GetById.Response>> GetPaged(Paged.Request request, CancellationToken cancellationToken);
    }
}