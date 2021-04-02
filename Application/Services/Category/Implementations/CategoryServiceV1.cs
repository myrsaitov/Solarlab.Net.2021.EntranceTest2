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

namespace WidePictBoard.Application.Services.Category.Implementations
{
    public sealed class CategoryServiceV1 : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;
        private PagedBase<Paged.Response<GetById.Response>, GetById.Response, Paged.Request, Domain.Category> _paged;

        public CategoryServiceV1(ICategoryRepository categoryRepository, IIdentityService identityService, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _identityService = identityService;
            _mapper = mapper;
        }

        public async Task<Create.Response> Create(Create.Request request, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<Domain.Category>(request);
            category.IsDeleted = false;
            category.CreatedAt = DateTime.UtcNow;

            var parentCategoryIdNulable = request.ParentCategoryId;

            await _categoryRepository.Save(category, cancellationToken);

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
                    await _categoryRepository.Save(category, cancellationToken);
                }
            }
            
            return new Create.Response
            {
                Id = category.Id
            };
        }

        public async Task<Update.Response> Update(Update.Request request, CancellationToken cancellationToken)
        {
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
            var category = await _categoryRepository.FindById(request.Id, cancellationToken);
            if (category == null)
            {
                throw new CategoryNotFoundException(request.Id);
            }

            return _mapper.Map<GetById.Response>(category);
        }

        public async Task<Paged.Response<GetById.Response>> GetPaged(Paged.Request request, CancellationToken cancellationToken)
        {
            _paged = new PagedBase<Paged.Response<GetById.Response>, GetById.Response, Paged.Request, Domain.Category>();
            return await _paged.GetPaged(request, _categoryRepository, _mapper, cancellationToken);
        }
    }
}