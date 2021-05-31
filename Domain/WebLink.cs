using System.Collections.Generic;
using SL2021.Domain.General;

namespace SL2021.Domain
{
    public class WebLink : EntityMutable<int>
    {
        // Адрес вебстраницы в интернет
        public string URL { get; set; }

        // Аккаунт, под которым записана эта страница
        public string OwnerId { get; set; }
        public virtual User Owner { get; set; }

        // Страницы, которые ссылаются на эту страницу
        public virtual ICollection<WebLink> ParentPages { get; set; }

        // Страницы, на которые ссылается эта страница
        public virtual ICollection<WebLink> ChildPages { get; set; }

        // Объявление в моей БД, которые соответсвует данной странице
        public int ContentId { get; set; }
        public virtual Content Content { get; set; }
    }
}