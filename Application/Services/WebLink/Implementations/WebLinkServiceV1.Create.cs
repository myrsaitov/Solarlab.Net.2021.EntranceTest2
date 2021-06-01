using System;
using System.Threading;
using System.Threading.Tasks;
using SL2021.Application.Services.WebLink.Contracts;
using SL2021.Application.Services.WebLink.Interfaces;

namespace SL2021.Application.Services.WebLink.Implementations
{
    public sealed partial class WebLinkServiceV1 : IWebLinkService
    {
        public async Task<Create.Response> Create(
            Create.Request request, 
            CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            //string userId = await _identityService.GetCurrentUserId(cancellationToken);

            // Ищем в БД эту ссылку
            var weblink = await _webLinkRepository.FindByURL(request.URL, cancellationToken);
            
            // Если ссылка не найдена, то создаем новый объект в БД
            if (weblink is null)
            {
                weblink = _mapper.Map<Domain.WebLink>(request);
                weblink.IsDeleted = false;
                weblink.IsSearched = false;
                //weblink.OwnerId = userId;
                weblink.CreatedAt = DateTime.UtcNow;

                await _webLinkRepository.Save(
                    weblink,
                    cancellationToken);
            }
            else
            {
                weblink.ReferedCount += 1;
                await _webLinkRepository.Save(
                    weblink,
                    cancellationToken);
            }

            return new Create.Response
            {
                Id = weblink.Id
            };
        }
    }
}