using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HtmlAgilityPack; //nuget HtmlAgilityPack
using System.Collections.Generic;
using System.Net;
using System;
using SL2021.Application.Services.WebLink.Contracts;

namespace SL2021.API.Controllers.Parser
{
    public partial class ParserController
    {

        //  Нет пересечений тоже показывать
        //  Leroy Merlin
        //  23 june deadline
        //  https://stroypark.su/
        //  https://leroymerlin.ru/
        //  https://vseinstrumenti.ru/
        //  https://petrovich.ru/

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetLinksFromPage(
            string URL,
            CancellationToken cancellationToken)
        {
            var request = new GetLinksFromPage.Request()
            {
                //URL = "https://stroypark.su/"
                URL = URL
            };

            return Ok(await _webLinkService.GetLinksFromPage(request, cancellationToken));
        }
    }
}