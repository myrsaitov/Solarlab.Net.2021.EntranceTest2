using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using SL2021.Domain.General;

namespace SL2021.Domain
{
    public class Content : EntityMutable<int>
    {
        public string Title { get; set; }
        public string CongratulationsText { get; set; }
        public User Person { get; set; }
        public DateTime HolidayDate { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public string OwnerId { get; set; }
        public virtual User Owner { get; set; }
      
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
    }
}