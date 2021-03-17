using System.Collections;
using System.Collections.Generic;
using WidePictBoard.Domain.General;

namespace WidePictBoard.Domain
{
    public class Category : EntityMutable<int>
    {
        public string Name { get; set; }

        public Category ParentCategory { get; set; }

        public ICollection<Category> ChildCategories { get; set; }












        public enum Statuses
        {
            Created,
            Payed,
            Closed
        }

        /// <summary>
        /// Цена
        /// </summary>
        public decimal Price { get; set; }

        public string OwnerId { get; set; }
        public User Owner { get; set; }
        public Statuses Status { get; set; }



        public ICollection<Comment> Comments { get; set; }

        public ICollection<Tag> Tags { get; set; }
    }
}