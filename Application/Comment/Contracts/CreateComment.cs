namespace WidePictBoard.Application.Comment.Contracts
{
    public static class CreateComment
    {
        public sealed class Request
        {
            public int AdId { get; set; }
            public string Text { get; set; }
        }

        public sealed class Response
        {
            public int Id { get; set; }
        }
    }
}