using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WidePictBoard.Application.Services.Category.Contracts;
using WidePictBoard.Application.Services.Category.Contracts.Exceptions;
using WidePictBoard.Application.Services.Category.Interfaces;

namespace WidePictBoard.Application.Services.Category.Implementations
{
    public sealed partial class CategoryServiceV1 : ICategoryService
    {
        public async Task<Paged.Response<GetById.Response>> GetPaged(
            Paged.Request request, 
            CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException();
            }
            var total = await _categoryRepository.Count(cancellationToken);

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

            var entities = await _categoryRepository.GetPaged(
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