namespace SL2021.Application.Services.Image.Contracts
{
    public static class GetPaged
    {
        public sealed class Response
        {
            public int Id { get; set; }
            public string URL { get; set; }
            public int ContentId { get; set; }
        }
    }
}