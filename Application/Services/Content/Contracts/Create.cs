namespace WidePictBoard.Application.Services.Content.Contracts
{
    public static class Create
    {

        public sealed class Request
        {
            public string Title { get; set; }
            public string Body { get; set; }




            public string Name { get; set; }
            public decimal Price { get; set; }
        }

        public sealed class Response
        {
            public int Id { get; set; }
        }
    }
}