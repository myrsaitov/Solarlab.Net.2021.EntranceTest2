using WidePictBoard.Domain.General;

namespace WidePictBoard.Domain.Content
{
    public class Content : EntityMutable<string>
    {
        public int ResH { get; set; }
        public int ResV { get; set; }
        
    }
}