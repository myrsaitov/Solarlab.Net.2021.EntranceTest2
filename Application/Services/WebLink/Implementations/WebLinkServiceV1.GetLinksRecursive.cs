using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SL2021.Application.Services.WebLink.Contracts;
using SL2021.Application.Services.WebLink.Interfaces;
using HtmlAgilityPack;
using System.Net;

namespace SL2021.Application.Services.WebLink.Implementations
{
    public sealed partial class WebLinkServiceV1 : IWebLinkService
    {
        public async Task<GetLinksRecursive.Response> GetLinksRecursive(
            GetLinksRecursive.Request request, 
            CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            // Получаем ссылки с первой страницы
            var page_request = new GetLinksFromPage.Request()
            {
                URL = request.TopURL
            };
            await GetLinksFromPage(page_request, cancellationToken);

            var weblink = await _webLinkRepository.FindNoneIndexed(cancellationToken);
            
            if(weblink is not null)
            {
                page_request.URL = weblink.URL;
                await GetLinksFromPage(page_request, cancellationToken);
            }

            return new GetLinksRecursive.Response();
        }
    }
}