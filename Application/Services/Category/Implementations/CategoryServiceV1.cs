using System;
using System.Threading;
using System.Threading.Tasks;
using MapsterMapper;
using WidePictBoard.Application.Common;
using WidePictBoard.Application.Identity.Interfaces;
using WidePictBoard.Application.Repositories;
using WidePictBoard.Application.Services.Category.Contracts;
using WidePictBoard.Application.Services.Category.Contracts.Exceptions;
using WidePictBoard.Application.Services.Category.Interfaces;
using WidePictBoard.Domain.General.Exceptions;
using WidePictBoard.Application.Services.PagedBase.Contracts;
using WidePictBoard.Application.Services.PagedBase.Implementations;
using System.Collections.Generic;
using System.Linq;

namespace WidePictBoard.Application.Services.Category.Implementations
{
    public sealed class CategoryServiceV1 : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;
        private PagedBase<GetById.Response, Domain.Category, int> _paged;

        public CategoryServiceV1(ICategoryRepository categoryRepository, IIdentityService identityService, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _identityService = identityService;
            _mapper = mapper;
        }

        public async Task<Create.Response> Create(Create.Request request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new CategoryCreateRequestIsNullException();
            }

            var userId = await _identityService.GetCurrentUserId(cancellationToken);
            var isAdmin = await _identityService.IsInRole(userId, RoleConstants.AdminRole, cancellationToken);
            if (!isAdmin)
            {
                throw new NoRightsException("Нет прав для выполнения операции.");
            }

            var category = _mapper.Map<Domain.Category>(request);
            category.IsDeleted = false;
            category.CreatedAt = DateTime.UtcNow;
            await _categoryRepository.Save(category, cancellationToken);

            var parentCategoryIdNulable = request.ParentCategoryId;
            if (parentCategoryIdNulable != null)
            {
                int parentCategoryId = (int)parentCategoryIdNulable;
                var parentCategory = await _categoryRepository.FindById(parentCategoryId, cancellationToken);
                if (parentCategory != null)
                {
                    if (parentCategory.ChildCategories != null)
                    {
                        parentCategory.ChildCategories.Add(category);
                    }
                    else
                    {
                        parentCategory.ChildCategories = new List<Domain.Category>()
                        {
                            category
                        };
                    }
                    await _categoryRepository.Save(parentCategory, cancellationToken);
                    
                    category.ParentCategory = parentCategory;
                }
                await _categoryRepository.Save(category, cancellationToken);
            }
            
            return new Create.Response
            {
                Id = category.Id
            };
        }

        public async Task<Update.Response> Update(Update.Request request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new CategoryUpdateRequestIsNullException();
            }

            var category = await _categoryRepository.FindById(request.Id, cancellationToken);
            if (category == null)
            {
                throw new CategoryNotFoundException(request.Id);
            }

            var userId = await _identityService.GetCurrentUserId(cancellationToken);
            var isAdmin = await _identityService.IsInRole(userId, RoleConstants.AdminRole, cancellationToken);

            if (!isAdmin)
            {
                throw new NoRightsException("Нет прав для выполнения операции.");
            }

            category = _mapper.Map<Domain.Category>(request);

            category.IsDeleted = false;
            category.UpdatedAt = DateTime.UtcNow;
            await _categoryRepository.Save(category, cancellationToken);

            return new Update.Response
            {
                Id = category.Id
            };
        }
        public async Task Delete(Delete.Request request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new CategoryDeleteRequestIsNullException();
            }

            var category = await _categoryRepository.FindById(request.Id, cancellationToken);
            if (category == null)
            {
                throw new CategoryNotFoundException(request.Id);
            }

            var userId = await _identityService.GetCurrentUserId(cancellationToken);
            var isAdmin = await _identityService.IsInRole(userId, RoleConstants.AdminRole, cancellationToken);

            if (!isAdmin)
            {
                throw new NoRightsException("Нет прав для выполнения операции.");
            }

            category.IsDeleted = true;
            category.UpdatedAt = DateTime.UtcNow;
            await _categoryRepository.Save(category, cancellationToken);
        }

        public async Task Restore(Restore.Request request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new CategoryRestoreRequestIsNullException();
            }

            var category = await _categoryRepository.FindById(request.Id, cancellationToken);
            if (category == null)
            {
                throw new CategoryNotFoundException(request.Id);
            }

            var userId = await _identityService.GetCurrentUserId(cancellationToken);
            var isAdmin = await _identityService.IsInRole(userId, RoleConstants.AdminRole, cancellationToken);

            if (!isAdmin)
            {
                throw new NoRightsException("Нет прав для выполнения операции.");
            }

            category.IsDeleted = false;
            category.UpdatedAt = DateTime.UtcNow;
            await _categoryRepository.Save(category, cancellationToken);
        }

        public async Task<GetById.Response> GetById(GetById.Request request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new CategoryGetByIdRequestIsNullException();
            }

            var category = await _categoryRepository.FindByIdWithParentAndChilds(request.Id, cancellationToken);
            if (category == null)
            {
                throw new CategoryNotFoundException(request.Id);
            }

            return _mapper.Map<GetById.Response>(category);
        }

        public async Task<Paged.Response<GetById.Response>> GetPaged(Paged.Request request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new CategoryGetPagedRequestIsNullException();
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
                request.Page, request.PageSize, cancellationToken
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