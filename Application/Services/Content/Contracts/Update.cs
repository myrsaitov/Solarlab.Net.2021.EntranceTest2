﻿using System;

namespace SL2021.Application.Services.Content.Contracts
{
    public static class Update
    {
        public sealed class Request
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Body { get; set; }
            public int CategoryId { get; set; }
            public string[] TagBodies { get; set; }
        }
        public sealed class Response
        {
            public int Id { get; set; }
        }
    }
}