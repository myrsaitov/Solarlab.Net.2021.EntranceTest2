using System.Collections.Generic;
using SL2021.Domain.General;

namespace SL2021.Domain
{
    public class Category : EntityMutable<int>
    {
        public string Name { get; set; }
        public int? ParentCategoryId { get; set; }
        public virtual Category ParentCategory { get; set; }
        public virtual ICollection<Category> ChildCategories { get; set; }
        public virtual ICollection<Content> Contents { get; set; }
    }
}