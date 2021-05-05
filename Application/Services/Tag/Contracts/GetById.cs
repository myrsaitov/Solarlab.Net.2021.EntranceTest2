namespace SL2021.Application.Services.Tag.Contracts
{
    public static class GetById
    {
        public sealed class Request
        {
            public int Id { get; set; }
        }

        public sealed class Response
        {
            public int Id { get; set; }
            public string Body { get; set; }
            public int Count { get; set; }
        }
    }
}