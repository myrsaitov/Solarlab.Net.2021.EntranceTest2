using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WidePictBoard.Application;
using WidePictBoard.Application.Services.Comment.Contracts;
using WidePictBoard.Application.Services.Comment.Interfaces;
using WidePictBoard.Application.Repositories;
using WidePictBoard.Application.Services.User.Contracts.Exceptions;
using WidePictBoard.Application.Services.User.Interfaces;

namespace WidePictBoard.Application.Services.Comment.Implementations
{
    public sealed class CommentServiceV1 : ICommentService
    {
        private readonly IRepository<Domain.Comment, int> _repository;
        private readonly IUserService _userService;

        public CommentServiceV1(IRepository<Domain.Comment, int> repository, IUserService userService)
        {
            _repository = repository;
            _userService = userService;
        }

        public async Task<CreateComment.Response> CreateComment(
            CreateComment.Request request,
            CancellationToken cancellationToken
        )
        {
            var user = await _userService.GetCurrent(cancellationToken);
            if (user == null)
            {
                throw new NoRightsException("НЕТ ПРАВ");
            }

            var comment = new Domain.Comment
            {
                AdId = request.AdId,
                Text = request.Text,
                AuthorId = user.Id
            };

            await _repository.Save(comment, cancellationToken);

            return new CreateComment.Response
            {
                Id = comment.Id
            };
        }

        public async Task<GetPagedComments.Response> GetPagedComments(GetPagedComments.Request request, CancellationToken cancellationToken)
        {
            var total = await _repository.Count(cancellationToken);

            if (total == 0)
            {
                return new GetPagedComments.Response
                {
                    Items = Array.Empty<GetPagedComments.Response.Item>(),
                    Limit = request.Limit,
                    Offset = request.Offset,
                    Total = 0
                };
            }

            var result = await _repository.GetPaged(request.Offset, request.Limit, cancellationToken);

            return new GetPagedComments.Response
            {
                Items = result.Select(i => new GetPagedComments.Response.Item
                {
                    Id = i.Id,
                    Text = i.Text,
                    AdId = i.AdId,
                    AuthorId = i.AuthorId,
                    AuthorName = i.Author?.Name
                }),
                Total = total,
                Limit = request.Limit,
                Offset = request.Offset
            };
        }
    }
}