using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WidePictBoard.Application.Services.Comment.Contracts;
using WidePictBoard.Application.Services.Comment.Contracts.Exceptions;
using WidePictBoard.Application.Services.Comment.Interfaces;
using WidePictBoard.Application.Services.Content.Contracts.Exceptions;

namespace WidePictBoard.Application.Services.Comment.Implementations
{
    public sealed partial class CommentServiceV1 : ICommentService
    {
        public async Task<Create.Response> Create(
            Create.Request request, 
            CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var contentRequest = new Content.Contracts.GetById.Request()
            {
                Id = request.ContentId
            };

            var content = await _contentRepository.FindByIdWithUserInclude(
                contentRequest.Id,
                cancellationToken);

            if (content == null)
            {
                throw new ContentNotFoundException(contentRequest.Id);
            };

            string userId = await _identityService.GetCurrentUserId(cancellationToken);

            var comment = _mapper.Map<Domain.Comment>(request); 
            comment.IsDeleted = false;
            comment.OwnerId = userId;
            comment.CreatedAt = DateTime.UtcNow;

            var parentCommentIdNulable = request.ParentCommentId;
            if (parentCommentIdNulable != null)
            {
                int parentCommentId = (int)parentCommentIdNulable;
                
                var parentComment = await _commentRepository.FindByIdWithUserAndCommentsInclude(
                    parentCommentId, 
                    cancellationToken);
                
                if (parentComment != null)
                {
                    if (parentComment.ChildComments != null)
                    {
                        parentComment.ChildComments.Add(comment);
                    }
                    else
                    {
                        parentComment.ChildComments = new List<Domain.Comment>()
                        {
                            comment
                        };
                    }
                    await _commentRepository.Save(parentComment, cancellationToken);

                    comment.ParentComment = parentComment;
                }
                else 
                {
                    throw new ParentCommentNotFoundException(request.ParentCommentId);
                }

                await _commentRepository.Save(comment, cancellationToken);
            }

            content.Comments = new List<Domain.Comment>()
            {
                comment
            };

            await _contentRepository.Save(content, cancellationToken);

            await _commentRepository.Save(comment, cancellationToken);

            return new Create.Response
            {
                Id = comment.Id
            };
        }
    }
}