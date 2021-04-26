namespace WidePictBoard.API.Controllers
{
    public class GetPagedTaggedRequest : GetPagedRequest
    {
        public string Tag { get; set; }
    }
}