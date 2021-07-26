﻿using System;
using System.Collections.Generic;

namespace SL2021.Application.Services.Content.Contracts
{
    public static class Create
    {
        public sealed class Request
        {
            public string Title { get; set; }
            public string CongratulationsText { get; set; }
            public int CategoryId { get; set; }
            public string OwnerId { get; set; }

            public string[] TagBodies { get; set; }
        }
        public sealed class Response
        {
            public int Id { get; set; }
        }
    }
}