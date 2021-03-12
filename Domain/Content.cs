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

        /// <summary>
        /// Цена
        /// </summary>
        public decimal Price { get; set; }

        public string OwnerId { get; set; }
        public User Owner { get; set; }
        public Statuses Status { get; set; }

        public Category Category { get; set; }
    }
}