using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WidePictBoard.Application.Services.Comment.Contracts;
using WidePictBoard.Application.Services.Comment.Contracts.Exceptions;
using WidePictBoard.Application.Services.Comment.Interfaces;
using WidePictBoard.Application.Services.Content.Contracts.Exceptions;

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
                throw new CommentGetPagedRequestIsNullException();
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

            if (total == 0)
            {
                return new Paged.Response<GetById.Response>
                {
                    Items = Array.Empty<GetById.Response>(),
                    Total = total,
                    Offset = request.Page,
                    Limit = request.PageSize
                };
            }

            var entities = await _commentRepository.GetPaged(
                a => a.ContentId == contentId,
                request.Page,
                request.PageSize,
                cancellationToken
            );

            return new Paged.Response<GetById.Response>
            {
                Items = entities.Select(entity => _mapper.Map<GetById.Response>(entity)),
                Total = total,
                Offset = request.Page,
                Limit = request.PageSize
            };
        }
    }
}