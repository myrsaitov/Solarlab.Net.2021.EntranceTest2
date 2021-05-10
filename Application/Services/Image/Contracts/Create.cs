namespace SL2021.Application.Services.Image.Contracts
{
    public static class Create
    {
        public sealed class Request
        {
            public string Title { get; set; }
            public string OwnerId { get; set; }
        }
        public sealed class Response
        {
            public int Id { get; set; }
        }
    }
}