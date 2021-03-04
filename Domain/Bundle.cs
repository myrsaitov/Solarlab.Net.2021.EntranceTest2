using System.Collections.Generic;
using WidePictBoard.Domain.General;

namespace WidePictBoard.Domain
{
    public class Bundle: EntityMutable<string>
    {
        public IEnumerable<Content> Items { get; set; }
    }
}