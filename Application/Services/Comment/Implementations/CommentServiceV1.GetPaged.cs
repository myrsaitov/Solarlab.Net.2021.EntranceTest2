using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SL2021.Application.Services.Comment.Contracts;
using SL2021.Application.Services.Comment.Interfaces;
using SL2021.Application.Services.Content.Contracts.Exceptions;
using SL2021.Application.Services.Contracts;
using SL2021.Application.Services.Extensions;

namespace SL2021.Application.Services.Comment.Implementations
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
            
            var entities = await _commentRepository.GetPagedWithOwnerAndCommentInclude(
                a => a.ContentId == contentId,
                offset,
                request.PageSize,
                cancellationToken
            );

            //var tree_entities = entities.ToList().ToTree(item => item.Id, item => item.ParentCommentId);

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