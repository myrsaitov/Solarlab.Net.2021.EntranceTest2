namespace SL2021.API.Controllers
{
    public class GetPagedContentRequest : GetPagedRequest
    {
        public string Tag { get; set; }
        public int? CategoryId { get; set; }
        public string UserName { get; set; }
    }
}