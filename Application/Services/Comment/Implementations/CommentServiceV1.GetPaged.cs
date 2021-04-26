using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WidePictBoard.Application.Services.Comment.Contracts;
using WidePictBoard.Application.Services.Comment.Contracts.Exceptions;
using WidePictBoard.Application.Services.Comment.Interfaces;
using WidePictBoard.Application.Services.Content.Contracts.Exceptions;
using WidePictBoard.Application.Services.Contracts;

namespace WidePictBoard.Application.Services.Comment.Implementations
{
    public sealed partial class CommentServiceV1 : ICommentService
    {
        public async Task<Paged.Response<GetById.Response>> GetPaged(
            int contentId,
            Paged.Request request,
            CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var content = await _contentRepository.FindById(
                contentId,
                cancellationToken);

            if (content == null)
            {
                throw new ContentNotFoundException(contentId);
            }

            var total = await _commentRepository.Count(
                a => a.ContentId == contentId,
                cancellationToken);

            var offset = request.Page * request.PageSize;

            if (total == 0)
            {
                return new Paged.Response<GetById.Response>
                {
                    Items = Array.Empty<GetById.Response>(),
                    Total = total,
                    Offset = offset,
                    Limit = request.PageSize
                };
            }

            var entities = await _commentRepository.GetPagedWithOwnerInclude(
                a => a.ContentId == contentId,
                offset,
                request.PageSize,
                cancellationToken
            );

            return new Paged.Response<GetById.Response>
            {
                Items = entities.Select(entity => _mapper.Map<GetById.Response>(entity)),
                Total = total,
                Offset = offset,
                Limit = request.PageSize
            };
        }
    }
}