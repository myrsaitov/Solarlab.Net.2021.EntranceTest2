using System;
using System.Threading;
using System.Threading.Tasks;
using SL2021.Application.Common;
using SL2021.Application.Services.Content.Contracts;
using SL2021.Application.Services.Content.Contracts.Exceptions;
using SL2021.Application.Services.Content.Interfaces;
using SL2021.Domain.General.Exceptions;

namespace SL2021.Application.Services.Content.Implementations
{
    public sealed partial class ContentServiceV1 : IContentService
    {
        public async Task Delete(
            Delete.Request request,
            CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var content = await _contentRepository.FindByIdWithUserAndTagsInclude(
                request.Id,
                cancellationToken);

            if (content == null)
            {
                throw new ContentNotFoundException(request.Id);
            }

            var userId = await _identityService.GetCurrentUserId(cancellationToken);

            var isAdmin = await _identityService.IsInRole(
                userId,
                RoleConstants.AdminRole,
                cancellationToken);

            if (!isAdmin && content.OwnerId != userId)
            {
                throw new NoRightsException("Нет прав для выполнения операции.");
            }

            content.IsDeleted = true;
            content.UpdatedAt = DateTime.UtcNow;

            // TODO Сделать нормальный подсчет количества Tags
            foreach (var tag in content.Tags)
            {
                if (tag.Count > 0)
                {
                    tag.Count -= 1;
                    await _tagRepository.Save(tag, cancellationToken);
                }
            }

            await _contentRepository.Save(content, cancellationToken);
        }
    }
}