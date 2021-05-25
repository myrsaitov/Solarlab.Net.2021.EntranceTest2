using Mapster;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SL2021.Application.Services.Image.Contracts;
using SL2021.Application.Services.Image.Interfaces;
using SL2021.Application.Services.Contracts;
using System.Linq.Expressions;

namespace SL2021.Application.Services.Image.Implementations
{
    public sealed partial class ImageServiceV1 : IImageService
    {
        public async Task<Paged.Response<GetPaged.Response>> GetPaged(
            Expression<Func<Domain.Image, bool>> predicate,
            Paged.Request request,
            CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var offset = request.Page * request.PageSize;

            var total = await _imageRepository.Count(predicate, cancellationToken);

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

            var entities = await _imageRepository.GetPaged(
                predicate,
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
    }
}