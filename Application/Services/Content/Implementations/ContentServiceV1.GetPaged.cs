using Mapster;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WidePictBoard.Application.Services.Content.Contracts;
using WidePictBoard.Application.Services.Content.Contracts.Exceptions;
using WidePictBoard.Application.Services.Content.Interfaces;

namespace WidePictBoard.Application.Services.Content.Implementations
{
    public sealed partial class ContentServiceV1 : IContentService
    {
        public async Task<Paged.Response<GetById.Response>> GetPaged(
            Paged.Request request,
            CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ContentGetPagedRequestIsNullException();
            }

            var total = await _contentRepository.Count(cancellationToken);

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

            var entities = await _contentRepository.GetPagedWithTagsInclude(
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
        public async Task<Paged.Response<GetById.Response>> GetPaged(
            string tag,
            Paged.Request request,
            CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ContentGetPagedRequestIsNullException();
            }

            var total = await _contentRepository.Count(
                a => a.Tags.Any(t => t.Body == tag),
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

            var entities = await _contentRepository.GetPagedWithTagsInclude(
                tag,
                request.Page,
                request.PageSize,
                cancellationToken
            );

            var ret = new Paged.Response<GetById.Response>
            {
                Items = entities.Select(entity => entity.Adapt<GetById.Response>()),
                Total = total,
                Offset = request.Page,
                Limit = request.PageSize
            };

            return ret;
        }
    }
}