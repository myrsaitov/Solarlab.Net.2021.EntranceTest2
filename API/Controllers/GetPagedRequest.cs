﻿namespace SL2021.API.Controllers
{
    public class GetPagedRequest
    {
        public int PageSize { get; set; } = 20;
        public int Page { get; set; } = 0;
    }
}