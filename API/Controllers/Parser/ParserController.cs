// @nuget: HtmlAgilityPack

using System;
using HtmlAgilityPack;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SL2021.Application.Services.Content.Interfaces;

namespace SL2021.API.Controllers.Parser
{
    [Route("api/v1/parser")]
    [ApiController]
    [Authorize]
    public partial class ParserController : ControllerBase
    {
        private readonly IContentService _contentService;
        public ParserController(IContentService contentService)
        {
            _contentService = contentService;
        }
    }
}