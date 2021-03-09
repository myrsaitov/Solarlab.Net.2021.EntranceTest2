namespace WidePictBoard.Application.Content.Contracts
{
    public static class GetPaged
    {
        public sealed class Request
        {
            int? CategoryId { get; set; }
            int Page { get; set; }
            int PageSize { get; set; }
        }
        
        public sealed class Response
        {

        }
    }
}