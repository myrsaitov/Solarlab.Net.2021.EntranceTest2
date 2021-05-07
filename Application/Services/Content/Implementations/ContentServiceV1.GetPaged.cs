using Mapster;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SL2021.Application.Services.Content.Contracts;
using SL2021.Application.Services.Content.Interfaces;
using SL2021.Application.Services.Contracts;
using System.Linq.Expressions;

namespace SL2021.Application.Services.Content.Implementations
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

            var offset = request.Page * request.PageSize;

            var total = await _contentRepository.Count(cancellationToken);

            if (total == 0)
            {
                return new Paged.Response<GetPaged.Response>
                {
                    Items = Array.Empty<GetPaged.Response>(),
                    Total = total,
                    Offset = offset,
                    Limit = request.PageSize
                };
            }

            var entities = await _contentRepository.GetPagedWithTagsAndOwnerAndCategoryInclude(
                offset, 
                request.PageSize, 
                cancellationToken
            );

            return new Paged.Response<GetPaged.Response>
            {
                Items = entities.Select(entity => _mapper.Map<GetPaged.Response>(entity)),
                Total = total,
                Offset = offset,
                Limit = request.PageSize
            };
        }
        public async Task<Paged.Response<GetPaged.Response>> GetPaged(
            Expression<Func<Domain.Content, bool>> predicate,
            Paged.Request request,
            CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var total = await _contentRepository.Count(
                predicate,
                cancellationToken);

            var offset = request.Page * request.PageSize;

            if (total == 0)
            {
                return new Paged.Response<GetPaged.Response>
                {
                    Items = Array.Empty<GetPaged.Response>(),
                    Total = total,
                    Offset = offset,
                    Limit = request.PageSize
                };
            }

            var entities = await _contentRepository.GetPagedWithTagsAndOwnerAndCategoryInclude(
                predicate,
                offset,
                request.PageSize,
                cancellationToken
            );

            return new Paged.Response<GetPaged.Response>
            {
                Items = entities.Select(entity => entity.Adapt<GetPaged.Response>()),
                Total = total,
                Offset = offset,
                Limit = request.PageSize
            };
        }

    }
}