using System.Collections.Generic;
using SL2021.Domain.General;

namespace SL2021.Domain
{
    public class WebLink : EntityMutable<int>
    {
        // Адрес вебстраницы в интернет
        public string URL { get; set; }

        // Была ли просмотрена эта ссылка на предмет поиска других ссылок
        public bool IsSearched { get; set; }

        // Сколько раз ссылались на эту страницу
        public int ReferedCount { get; set; }

        // Аккаунт, под которым записана эта страница
        //public string OwnerId { get; set; }
        //public virtual User Owner { get; set; }

        // Страницы, которые ссылаются на эту страницу
        //public virtual ICollection<WebLink> ParentWebLinks { get; set; }

        // Страницы, на которые ссылается эта страница
        //public virtual ICollection<WebLink> ChildWebLinks { get; set; }

        // Объявление в моей БД, которые соответсвует данной странице
        //public int ContentId { get; set; }
        //public virtual Content Content { get; set; }
    }
}