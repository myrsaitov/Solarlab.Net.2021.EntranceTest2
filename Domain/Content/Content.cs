using System.Collections.Generic;
using WidePictBoard.Domain.General;

namespace WidePictBoard.Domain.Content
{
    public class Content : EntityMutable<string>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Formats { get; set; } // "svg;png;jpg"
        public int ResH { get; set; }
        public int ResV { get; set; }
        public IEnumerable<string> Tags { get; set; }
    }
}