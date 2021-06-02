using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SL2021.Application.Services.WebLink.Contracts;
using SL2021.Application.Services.WebLink.Interfaces;
using HtmlAgilityPack;
using System.Net;
using System.Linq;

namespace SL2021.Application.Services.WebLink.Implementations
{
    public sealed partial class WebLinkServiceV1 : IWebLinkService
    {
        public async Task<GetLinksFromPage.Response> GetLinksFromPage(
            GetLinksFromPage.Request request, 
            CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            // Если страница уже проиндексирована, то стоп
            var weblink_check = await _webLinkRepository.FindByURL(request.URL, cancellationToken);
            if (weblink_check is not null)
            {
                if (weblink_check.IsIndexed == true)
                {
                    return new GetLinksFromPage.Response();
                }
            }

            var ress = new List<string>();
            Uri myUri = new Uri(request.URL);                                   // "https://www.domain.com/index.htm"

            string target_host_domain = myUri.Host;                             // => "www.domain.com";
            string target_host_protocol = myUri.GetLeftPart(UriPartial.Scheme); // => "https://";
            string target_host = myUri.GetLeftPart(UriPartial.Authority);       // => "https://www.domain.com

            // Если сеть работает через прокси, то нужен этот блок
            //----------------------------------------------------
            var web = new HtmlWeb();
            web.PreRequest = delegate (HttpWebRequest webRequest)
            {
                webRequest.Timeout = 1200000;
                return true;
            };
            var proxy = new WebProxy() { UseDefaultCredentials = true };
            var doc = web.Load(request.URL, "GET", proxy, CredentialCache.DefaultNetworkCredentials);
            //----------------------------------------------------

            var create_request = new Create.Request();

            try
            {
                if (doc.DocumentNode.SelectNodes("//a[@href]") is not null)
                {
                    foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//a[@href]"))
                    {
                        // Получить адрес гиперссылки
                        string hrefValue = link.GetAttributeValue("href", string.Empty);


                        // Если гиперрсылка начинается с исследуемого адреса
                        if (hrefValue.StartsWith(target_host))
                        {
                            ress.Add(hrefValue);
                        }
                        // Если гиперссылка "относительная" (начинается с "//")  "//stroypark.su"
                        else if (hrefValue.StartsWith("//" + target_host_domain))
                        {
                            // TODO сделать нормально
                            string res = target_host_protocol + ":" + hrefValue;
                            ress.Add(res);
                        }
                        // Если гиперссылка "относительная" (начинается с "/")  "/Catalog/..."
                        else if (hrefValue.StartsWith("/"))
                        {
                            // Если в конце адреса вдруг поставили "/"
                            if (target_host.EndsWith("/"))
                            {
                                string res = target_host.Remove(target_host.Length - 1) + hrefValue;
                                ress.Add(res);
                            }
                            else
                            {
                                // Если в конце адреса не поставили "/"

                                string res = target_host + hrefValue;
                                ress.Add(res);
                            }
                        }
                    }
                    foreach (var URL in ress)
                    {
                        Uri tempUri = new Uri(URL);
                        string tempHost = tempUri.Host;

                        // Внешние ссылки отсекаем
                        if (target_host_domain == tempHost)
                        {
                            create_request.URL = URL;
                            await Create(create_request, cancellationToken); 
                        }
                    }
                }
                else
                {
                    ress.Add("There are no Hyperlinks!");
                }

                // Ищем в базе стартовую ссылку и присваиваем ей статус "Проиндексировано"
                var weblink = await _webLinkRepository.FindByURL(request.URL, cancellationToken);
                if (weblink is not null)
                {
                    weblink.IsIndexed = true;
                    await _webLinkRepository.Save(weblink, cancellationToken);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return new GetLinksFromPage.Response();
        }
    }
}