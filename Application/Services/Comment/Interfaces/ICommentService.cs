using System.Threading;
using System.Threading.Tasks;
using WidePictBoard.Application.Services.Comment.Contracts;

namespace WidePictBoard.Application.Services.Comment.Interfaces
{
    public interface ICommentService
    {
        Task<CreateComment.Response> CreateComment(CreateComment.Request request, CancellationToken cancellationToken);

        Task<GetPagedComments.Response> GetPagedComments(GetPagedComments.Request request,
            CancellationToken cancellationToken);
    }
}