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

namespace SL2021.API.Controllers.Parser
{
    public partial class ParserController
    {

        //Нет пересечений тоже показывать
        //Leroy Merlin
        //23 june deadline

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(
            string Text, 
            CancellationToken cancellationToken)
        {
            //var doc = new HtmlAgilityPack.HtmlDocument();
            //HtmlWeb web = new HtmlWeb();
            
            var ress = new List<string>();


            HtmlWeb hw = new HtmlWeb();
            HtmlDocument doc = hw.Load("https://stroypark.su/");
            foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//a[@href]"))
            {
                string hrefValue = link.GetAttributeValue("href", string.Empty);
                ress.Add(hrefValue);
            }



            //doc = web.Load("https://stroypark.su/");

            //string res = doc.DocumentNode.SelectSingleNode($"//*[text()[contains(., '{Text}')]]").InnerText;

            //ress.Add(res);

            return Ok(ress);

            /*
                        string res = doc.DocumentNode.SelectSingleNode($"//*[text()[contains(., '{Text}')]]").InnerText;
                        if (ress is not null)
                        {
                            ress.Add(res);
                        }
                        else
                        {
                            ress.Add("null");
                        }

                        doc = web.Load("https://petrovich.ru/");
                        res = doc.DocumentNode.SelectSingleNode($"//*[text()[contains(., '{Text}')]]").InnerText;
                        if (ress is not null)
                        {
                            ress.Add(res);
                        }
                        else
                        {
                            ress.Add("null");
                        }

                        doc = web.Load("https://www.vseinstrumenti.ru/");
                        res = doc.DocumentNode.SelectSingleNode($"//*[text()[contains(., '{Text}')]]").InnerText;
                        if (ress is not null)
                        {
                            ress.Add(res);
                        }
                        else
                        {
                            ress.Add("null");
                        }*/


        }
    }
}