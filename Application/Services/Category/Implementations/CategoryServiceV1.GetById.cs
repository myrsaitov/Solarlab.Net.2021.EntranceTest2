using System;
using System.Threading;
using System.Threading.Tasks;
using SL2021.Application.Services.Category.Contracts;
using SL2021.Application.Services.Category.Contracts.Exceptions;
using SL2021.Application.Services.Category.Interfaces;

namespace SL2021.Application.Services.Category.Implementations
{
    public sealed partial class CategoryServiceV1 : ICategoryService
    {
        public async Task<GetById.Response> GetById(
            GetById.Request request,
            CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var category = await _categoryRepository.FindByIdWithParentAndChilds(
                request.Id, 
                cancellationToken);

            if (category == null)
            {
                throw new CategoryNotFoundException(request.Id);
            }

            return _mapper.Map<GetById.Response>(category);
        }
    }
}