using MapsterMapper;
using WidePictBoard.Application.Repositories;
using WidePictBoard.Application.Services.Tag.Interfaces;

namespace WidePictBoard.Application.Services.Tag.Implementations
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