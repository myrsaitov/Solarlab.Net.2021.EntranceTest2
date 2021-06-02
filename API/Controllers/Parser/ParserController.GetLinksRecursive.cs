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

        [HttpPut]
        [AllowAnonymous]
        public async Task<IActionResult> GetLinksRecursive(
            string TopURL,
            CancellationToken cancellationToken)
        {
            var request = new GetLinksRecursive.Request()
            {
                TopURL = "https://stroypark.su/"
                //TopURL = TopURL
            };

            return Ok(await _webLinkService.GetLinksRecursive(request, cancellationToken));
        }
    }
}