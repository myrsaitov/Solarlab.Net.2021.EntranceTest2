using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HtmlAgilityPack; //nuget HtmlAgilityPack
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

            string target_host_domain = "stroypark.su";
            string target_host_protocol = "https";

            string target_host = target_host_protocol + "://" + target_host_domain;

            // If there is a proxy, you will need this block
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