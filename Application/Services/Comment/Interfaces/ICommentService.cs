using System.Threading;
using System.Threading.Tasks;
using WidePictBoard.Application.Services.Comment.Contracts;
using WidePictBoard.Application.Services.PagedBase.Contracts;

namespace WidePictBoard.Application.Services.Comment.Interfaces
{
    public interface ICommentService
    {
        Task<Create.Response> Create(Create.Request request, CancellationToken cancellationToken);
        Task<Update.Response> Update(Update.Request request, CancellationToken cancellationToken);
        Task Delete(Delete.Request request, CancellationToken cancellationToken);
        Task Restore(Restore.Request request, CancellationToken cancellationToken);
        Task<GetById.Response> GetById(GetById.Request request, CancellationToken cancellationToken);
        Task<Paged.Response<GetById.Response>> GetPaged(int ContentId, Paged.Request request, CancellationToken cancellationToken);
    }
}