using System.Collections.Generic;
using SL2021.Domain.General;

namespace SL2021.Domain
{
    public class Image : EntityMutable<int>
    {
        public string URL { get; set; }
        public byte[] ImageData { get; set; }
        public int ContentId { get; set; }
        public virtual Content Content { get; set; }
    }
}