using System.Collections.Generic;
using WidePictBoard.Domain.General;

namespace WidePictBoard.Domain
{
    public class Content : EntityMutable<int>
    {
        public enum Statuses
        {
            Created,
            Payed,
            Closed
        }

        public string Title { get; set; }
        public string Body { get; set; }


        public decimal Price { get; set; }

        public string OwnerId { get; set; }
        public User Owner { get; set; }
        public Statuses Status { get; set; }

        public Category Category { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<Tag> Tags { get; set; }
    }
}