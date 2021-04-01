using WidePictBoard.Domain.General;

namespace WidePictBoard.Domain
{
    public class TagContent : EntityMutable<int>
    {
        public int? ContentId { get; set; }
        public virtual Content Content { get; set; }

        public int? TagId { get; set; }
        public virtual Tag Tag { get; set; }
    }
}