using System.Collections.Generic;
using WidePictBoard.Domain.General;

namespace WidePictBoard.Domain
{
    public class Content : EntityMutable<int>
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public decimal Price { get; set; }
        public string OwnerId { get; set; }
        public virtual User Owner { get; set; }
      
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
    }
}