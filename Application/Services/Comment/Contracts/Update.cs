using System;
using WidePictBoard.Domain.General;

namespace WidePictBoard.Application.Services.Comment.Contracts
{
    public static class Update
    {
        public sealed class Request
        {
            public int Id { get; set; }
            public string Body { get; set; }
        }
        public sealed class Response
        {
            public int Id { get; set; }
        }
    }
}