using Mapster;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WidePictBoard.Application.Services.Content.Contracts;
using WidePictBoard.Application.Services.Content.Interfaces;

namespace WidePictBoard.Application.Services.Content.Implementations
{
    public sealed partial class ContentServiceV1 : IContentService
    {
        public async Task<Paged.Response<GetPaged.Response>> GetPaged(
            Paged.Request request,
            CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var total = await _contentRepository.Count(cancellationToken);

            if (total == 0)
            {
                return new Paged.Response<GetPaged.Response>
                {
                    Items = Array.Empty<GetPaged.Response>(),
                    Total = total,
                    Offset = request.Page,
                    Limit = request.PageSize
                };
            }

            var entities = await _contentRepository.GetPagedWithTagsAndOwnerAndCategoryInclude(
                request.Page, 
                request.PageSize, 
                cancellationToken
            );

            return new Paged.Response<GetPaged.Response>
            {
                Items = entities.Select(entity => _mapper.Map<GetPaged.Response>(entity)),
                Total = total,
                Offset = request.Page,
                Limit = request.PageSize
            };
        }
        public async Task<Paged.Response<GetPaged.Response>> GetPaged(
            string tag,
            Paged.Request request,
            CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var total = await _contentRepository.Count(
                a => a.Tags.Any(t => t.Body == tag),
                cancellationToken);

            if (total == 0)
            {
                return new Paged.Response<GetPaged.Response>
                {
                    Items = Array.Empty<GetPaged.Response>(),
                    Total = total,
                    Offset = request.Page,
                    Limit = request.PageSize
                };
            }

            var entities = await _contentRepository.GetPagedWithTagsAndOwnerAndCategoryInclude(
                tag,
                request.Page,
                request.PageSize,
                cancellationToken
            );

            return new Paged.Response<GetPaged.Response>
            {
                Items = entities.Select(entity => entity.Adapt<GetPaged.Response>()),
                Total = total,
                Offset = request.Page,
                Limit = request.PageSize
            };
        }
    }
}