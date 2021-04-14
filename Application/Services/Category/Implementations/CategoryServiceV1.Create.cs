using System;
using System.Threading;
using System.Threading.Tasks;
using WidePictBoard.Application.Common;
using WidePictBoard.Application.Services.Category.Contracts;
using WidePictBoard.Application.Services.Category.Contracts.Exceptions;
using WidePictBoard.Application.Services.Category.Interfaces;
using WidePictBoard.Domain.General.Exceptions;
using System.Collections.Generic;

namespace WidePictBoard.Application.Services.Category.Implementations
{
    public sealed partial class CategoryServiceV1 : ICategoryService
    {
        public async Task<Create.Response> Create(
            Create.Request request, 
            CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException();
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
    }
}