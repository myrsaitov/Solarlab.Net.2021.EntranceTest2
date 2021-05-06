namespace SL2021.API.Controllers
{
    public class GetPagedContentRequest : GetPagedRequest
    {
        public string Tag { get; set; }
        public int? CategoyId { get; set; }
        public string UserName { get; set; }
    }
}