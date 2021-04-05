using System.Threading;
using System.Threading.Tasks;
using MapsterMapper;
using WidePictBoard.Application.Identity.Interfaces;
using WidePictBoard.Application.Repositories;
using WidePictBoard.Application.Services.PagedBase.Contracts;
using WidePictBoard.Application.Services.PagedBase.Implementations;
using WidePictBoard.Application.Services.Tag.Interfaces;
using WidePictBoard.Application.Services.Tag.Contracts;
using WidePictBoard.Application.Services.Content.Interfaces;

namespace WidePictBoard.Application.Services.Tag.Implementations
{
    public sealed class TagServiceV1 : ITagService
    {
        private readonly ITagRepository _repository;
        private readonly IMapper _mapper;
        private PagedBase<GetById.Response, Domain.Tag, int> _paged;

        public TagServiceV1(ITagRepository repository, IIdentityService identityService, IMapper mapper, IContentService contentService)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Paged.Response<GetById.Response>> GetPaged(Paged.Request request, CancellationToken cancellationToken)
        {
            _paged = new PagedBase<GetById.Response, Domain.Tag, int>();
            return await _paged.GetPaged(request, _repository, _mapper, cancellationToken);
        }
    }
}