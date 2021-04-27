using System;

namespace SL2021.Application.Services.Tag.Contracts
{
    public static class Create
    {
        public sealed class Request
        {
            public string Body { get; set; }
        }

        public sealed class Response
        {
            public int Id { get; set; }
        }
    }
}