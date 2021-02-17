namespace WidePictBoard.Application.Content.Contracts
{
    public static class Update
    {
        public sealed class Request
        {
            public string Title { get; set; }

            public string Body { get; set; }

            public string Email { get; set; }

            public bool Deleted { get; set; }

            public int CategoryId { get; set; }
        }
        
        public sealed class Response
        {
           // int Id { get; set; }

        }
    }
}