using System;
using System.Threading;
using System.Threading.Tasks;
using SL2021.Application.Services.Comment.Contracts;
using SL2021.Application.Services.Comment.Contracts.Exceptions;
using SL2021.Application.Services.Comment.Interfaces;

namespace SL2021.Application.Services.Comment.Implementations
{
    public sealed partial class CommentServiceV1 : ICommentService
    {
        public async Task<GetById.Response> GetById(
            GetById.Request request,
            CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
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