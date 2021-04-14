using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WidePictBoard.Application.Common;
using WidePictBoard.Application.Services.Comment.Contracts;
using WidePictBoard.Application.Services.Comment.Contracts.Exceptions;
using WidePictBoard.Application.Services.Comment.Interfaces;
using WidePictBoard.Domain.General.Exceptions;

namespace WidePictBoard.Application.Services.Comment.Implementations
{
    public sealed partial class CommentServiceV1 : ICommentService
    {
        public async Task<Update.Response> Update(
            Update.Request request,
            CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException();
            }

            var comment = await _commentRepository.FindByIdWithUserInclude(
                request.Id,
                cancellationToken);

            if (comment == null)
            {
                throw new CommentNotFoundException(request.Id);
            }

            var userId = await _identityService.GetCurrentUserId(cancellationToken);

            var isAdmin = await _identityService.IsInRole(
                userId,
                RoleConstants.AdminRole,
                cancellationToken);

            if (!isAdmin && comment.OwnerId != userId)
            {
                throw new NoRightsException("Нет прав для выполнения операции.");
            }

            comment.Body = request.Body;
            comment.IsDeleted = false;
            comment.UpdatedAt = DateTime.UtcNow;
            await _commentRepository.Save(comment, cancellationToken);

            return new Update.Response
            {
                Id = comment.Id
            };
        }
    }
}