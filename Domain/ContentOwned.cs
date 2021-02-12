namespace WidePictBoard.Domain
{
    public class ContentOwned 
    {
        public string ContentId { get; set; }
        public string OwnerId { get; set; }
        public Content Content { get; set; }
        public User Owner { get; set; }
    }
}