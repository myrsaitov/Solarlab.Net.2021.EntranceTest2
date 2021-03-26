using MapsterMapper;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WidePictBoard.Application.Common;
using WidePictBoard.Application.Identity.Interfaces;
using WidePictBoard.Application.Repositories;
using WidePictBoard.Application.Services.PagedBase.Contracts;
using WidePictBoard.Application.Services.PagedBase.Interfaces;
using WidePictBoard.Domain.General;
using WidePictBoard.Domain.General.Exceptions;


namespace WidePictBoard.Application.Services.PagedBase.Implementations
{
    public class PagedBase<TEntity>
        : IPagedBase<TEntity>
        where TEntity : EntityMutable<int>
    {
        private readonly IRepository<TEntity, int> _repository;
        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;
        public PagedBase(IRepository<TEntity, int> repository, IIdentityService identityService, IMapper mapper)
        {
            _repository = repository;
            _identityService = identityService;
            _mapper = mapper;
        }
        public async Task<Paged.Response<TEntity>> GetPaged(Paged.Request request, CancellationToken cancellationToken)
        {
            var total = await _repository.Count(
                cancellationToken
            );

            if (total == 0)
            {
                return new GetPaged.Response
                {
                    Items = Array.Empty<GetPaged.Response.ContentResponse>(),
                    Total = total,
                    Offset = request.Page,
                    Limit = request.PageSize
                };
            }

            var contents = await _repository.GetPaged(
                request.Page, request.PageSize, cancellationToken
            );

            return new GetPaged.Response
            {
                Items = contents.Select(content => _mapper.Map<GetPaged.Response.ContentResponse>(content)),
                Total = total,
                Offset = request.Page,
                Limit = request.PageSize
            };
        }
    }
}