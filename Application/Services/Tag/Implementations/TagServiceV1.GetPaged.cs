using System.Threading;
using System.Threading.Tasks;
using WidePictBoard.Application.Services.Tag.Interfaces;
using WidePictBoard.Application.Services.Tag.Contracts;
using System;
using System.Linq;
using WidePictBoard.Application.Services.Tag.Contracts.Exceptions;

namespace WidePictBoard.Application.Services.Tag.Implementations
{
    public sealed partial class TagServiceV1 : ITagService
    {
        public async Task<Paged.Response<GetById.Response>> GetPaged(
            Paged.Request request, 
            CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException();
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
                request.Page,
                request.PageSize,
                cancellationToken);

             
            var f = new Paged.Response<GetById.Response>
            {
                Items = entities.Select(entity => _mapper.Map<GetById.Response>(entity)),
                Total = total,
                Offset = request.Page,
                Limit = request.PageSize
            };
            f.Items.Count();

            return f;
        }
    }
}