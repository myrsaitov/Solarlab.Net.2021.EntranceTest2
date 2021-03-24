using System.Collections;
using System.Collections.Generic;
using WidePictBoard.Domain.General;

namespace WidePictBoard.Domain
{
    public class Category : EntityMutable<int>
    {

        public enum Statuses
        {
            Created,
            Closed
        }
        public string Name { get; set; }

        public Category ParentCategory { get; set; }
        public int? ParentCategoryId { get; set; }
        public ICollection<Category> ChildCategories { get; set; }
        public Statuses Status { get; set; }
    }
}