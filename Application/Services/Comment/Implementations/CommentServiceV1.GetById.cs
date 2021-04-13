using System.Threading;
using System.Threading.Tasks;
using WidePictBoard.Application.Services.Comment.Contracts;
using WidePictBoard.Application.Services.Comment.Contracts.Exceptions;
using WidePictBoard.Application.Services.Comment.Interfaces;

namespace WidePictBoard.Application.Services.Comment.Implementations
{
    public sealed partial class CommentServiceV1 : ICommentService
    {
        public async Task<GetById.Response> GetById(
            GetById.Request request,
            CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new CommentGetByIdRequestIsNullException();
            }

            var comment = await _commentRepository.FindByIdWithUserAndCommentsInclude(
                request.Id,
                cancellationToken);

            if (comment == null)
            {
                throw new CommentNotFoundException(request.Id);
            }

            return _mapper.Map<GetById.Response>(comment);
        }
    }
}