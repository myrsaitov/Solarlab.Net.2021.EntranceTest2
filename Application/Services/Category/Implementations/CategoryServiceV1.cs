using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WidePictBoard.Application.Common;
using WidePictBoard.Application.Identity.Interfaces;
using WidePictBoard.Application.Repositories;
using WidePictBoard.Application.Services.Category.Contracts;
using WidePictBoard.Application.Services.Category.Contracts.Exceptions;
using WidePictBoard.Application.Services.Category.Interfaces;
using WidePictBoard.Application.Services.User.Interfaces;
using WidePictBoard.Domain.General.Exceptions;

namespace WidePictBoard.Application.Services.Category.Implementations
{
    public sealed class CategoryServiceV1 : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IIdentityService _identityService;

        public CategoryServiceV1(ICategoryRepository repository, IIdentityService identityService)
        {
            _repository = repository;
            _identityService = identityService;
        }

        public async Task<Create.Response> Create(Create.Request request, CancellationToken cancellationToken)
        {
            //string userId = await _identityService.GetCurrentUserId(cancellationToken);
            var category = new Domain.Category
            {
                Name = request.Name,
                Status = Domain.Category.Statuses.Created,

                //Если раскомментировать, то ошибка FOREIGN KEY SAME TABLE
                ParentCategoryId_ = request.ParentCategoryId,
                
                
                
                CreatedAt = DateTime.UtcNow
            };
            await _repository.Save(category, cancellationToken);

            return new Create.Response
            {
                Id = category.Id
            };
        }

        public async Task Delete(Delete.Request request, CancellationToken cancellationToken)
        {
            var category = await _repository.FindByIdWithUserInclude(request.Id, cancellationToken);
            if (category == null)
            {
                throw new CategoryNotFoundException(request.Id);
            }

            var userId = await _identityService.GetCurrentUserId(cancellationToken);
            var isAdmin = await _identityService.IsInRole(userId, RoleConstants.AdminRole, cancellationToken);

           // if (!isAdmin && category.OwnerId != userId)
          //  {
           //     throw new NoRightsException("Нет прав для выполнения операции.");
          //  }

            category.Status = Domain.Category.Statuses.Closed;
            category.UpdatedAt = DateTime.UtcNow;
            await _repository.Save(category, cancellationToken);
        }

        public async Task<GetById.Response> GetById(GetById.Request request, CancellationToken cancellationToken)
        {
            var category = await _repository.FindByIdWithUserInclude(request.Id, cancellationToken);
            if (category == null)
            {
                throw new CategoryNotFoundException(request.Id);
            }

            return new GetById.Response
            {
                Id = category.Id,
                Name = category.Name,
                ParentCategoryId = category.ParentCategoryId_
            };
        }

        public async Task<GetAll.Response> GetAll(GetAll.Request request, CancellationToken cancellationToken)
        {
            var total = await _repository.Count(
                cancellationToken
            );

            if (total == 0)
            {
                return new GetAll.Response
                {
                    Items = Array.Empty<GetAll.Response.CategoryResponse>(),
                    Total = total,
                    Offset = request.Offset,
                    Limit = request.Limit
                };
            }

            var categories = await _repository.GetPaged(
                request.Offset, request.Limit, cancellationToken
            );


            return new GetAll.Response
            {
                Items = categories.Select(category => new GetAll.Response.CategoryResponse
                {
                    Id = category.Id,
                    Name = category.Name,
                    ParentId = category.ParentCategoryId_,
                    Status = category.Status.ToString()
                }),
                Total = total,
                Offset = request.Offset,
                Limit = request.Limit
            };
        }
    }
}