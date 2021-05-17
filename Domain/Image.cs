using System.Collections.Generic;
using SL2021.Domain.General;

namespace SL2021.Domain
{
    public class Image : EntityMutable<int>
    {
        public string URL { get; set; }
        public byte[] ImageData { get; set; }
        public string OwnerId { get; set; }
        public virtual User Owner { get; set; }
    }
}