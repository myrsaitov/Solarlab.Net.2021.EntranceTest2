using System.Threading;
using System.Threading.Tasks;
using MapsterMapper;
using WidePictBoard.Application.Repositories;
using WidePictBoard.Application.Services.PagedBase.Contracts;
using WidePictBoard.Application.Services.Tag.Interfaces;
using WidePictBoard.Application.Services.Tag.Contracts;
using System;
using System.Linq;
using WidePictBoard.Application.Services.Tag.Contracts.Exceptions;

namespace WidePictBoard.Application.Services.Tag.Implementations
{
    public sealed class TagServiceV1 : ITagService
    {
        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;

        public TagServiceV1(ITagRepository repository, IMapper mapper)
        {
            _tagRepository = repository;
            _mapper = mapper;
        }

        public async Task<Paged.Response<GetById.Response>> GetPaged(Paged.Request request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new TagGetPagedRequestIsNullException();
            }

            var total = await _tagRepository.Count(cancellationToken);

            if (total == 0)
            {
                return new Paged.Response<GetById.Response>
                {
                    Items = Array.Empty<GetById.Response>(),
                    Total = total,
                    Offset = request.Page,
                    Limit = request.PageSize
                };
            }

            var entities = await _tagRepository.GetPaged(
                request.Page, request.PageSize, cancellationToken
            );

            return new Paged.Response<GetById.Response>
            {
                Items = entities.Select(entity => _mapper.Map<GetById.Response>(entity)),
                Total = total,
                Offset = request.Page,
                Limit = request.PageSize
            };
        }
    }
}