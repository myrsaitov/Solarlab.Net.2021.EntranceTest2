using System.Threading;
using System.Threading.Tasks;
using WidePictBoard.Application.Services.Content.Contracts;

namespace WidePictBoard.Application.Services.Content.Interfaces
{
    public interface IContentService
    {
        Task<Create.Response> Create(Create.Request request, CancellationToken cancellationToken);

        Task Pay(Pay.Request request, CancellationToken cancellationToken);

        Task Delete(Delete.Request request, CancellationToken cancellationToken);

        Task<GetById.Response> Get(GetById.Request request, CancellationToken cancellationToken);
        Task<GetPaged.Response> GetPaged(GetPaged.Request request, CancellationToken cancellationToken);
    }
}