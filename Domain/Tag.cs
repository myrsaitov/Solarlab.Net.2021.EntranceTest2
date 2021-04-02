using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WidePictBoard.Domain.General;

namespace WidePictBoard.Domain
{
    public class Tag : EntityMutable<int>
    {
        [MaxLength(32)]
        public string Body { get; set; }
        public virtual ICollection<Content> Contents { get; set; }
    }
}