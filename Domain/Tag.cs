using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SL2021.Domain.General;

namespace SL2021.Domain
{
    public class Tag : Entity<int>
    {
        [MaxLength(32)]
        public string Body { get; set; }
        public int Count { get; set; }
        public virtual ICollection<Content> Contents { get; set; }
    }
}