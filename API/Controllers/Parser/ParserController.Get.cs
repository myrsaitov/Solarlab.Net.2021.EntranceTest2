using System.Threading;
using System.Threading.Tasks;
using SL2021.Application.Services.Content.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SL2021.Application.Services.Contracts;
using System.Linq;
using HtmlAgilityPack; //nuget HtmlAgilityPack
using TurnerSoftware.SitemapTools; ///nuget TurnerSoftware.SitemapTools
using System.Collections.Generic;
using System.Net;
using System;

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
        public async Task<IActionResult> GetById(
            string Text,
            CancellationToken cancellationToken)
        {
            var ress = new List<string>();

            string target_host = "https://stroypark.su";

            // If there is proxy, you need this block
            //----------------------------------------------------
            var web = new HtmlWeb();
            web.PreRequest = delegate (HttpWebRequest webRequest)
            {
                webRequest.Timeout = 1200000;
                return true;
            };
            var proxy = new WebProxy() { UseDefaultCredentials = true };
            var doc = web.Load(target_host, "GET", proxy, CredentialCache.DefaultNetworkCredentials);
            //----------------------------------------------------

            try
            {
                if (doc.DocumentNode.SelectNodes("//a[@href]") is not null)
                {
                    foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//a[@href]"))
                    {
                        string hrefValue = link.GetAttributeValue("href", string.Empty);
                        if (hrefValue.StartsWith(target_host))
                        {
                            ress.Add(hrefValue);
                        }
                        else if (hrefValue.StartsWith("/"))
                        {
                            if (target_host.EndsWith("/"))
                            {
                                string res = target_host.Remove(target_host.Length - 1) + hrefValue;
                                ress.Add(res);
                            }
                            else
                            {
                                string res = target_host + hrefValue;
                                ress.Add(res);
                            }
                        }
                    }
                }
                else
                {
                    ress.Add("There are no Hyperlinks!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return Ok(ress);
        }
    }
}