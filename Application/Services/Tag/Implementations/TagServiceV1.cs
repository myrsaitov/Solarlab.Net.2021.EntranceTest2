using MapsterMapper;
using SL2021.Application.Repositories;
using SL2021.Application.Services.Tag.Interfaces;

namespace SL2021.Application.Services.Tag.Implementations
{
    public sealed partial class TagServiceV1 : ITagService
    {
        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;

        public TagServiceV1(ITagRepository repository, IMapper mapper)
        {
            _tagRepository = repository;
            _mapper = mapper;
        }
    }
}