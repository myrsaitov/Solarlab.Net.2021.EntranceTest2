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

        public string Name { get; set; }
        public decimal Price { get; set; }

        public int OwnerId { get; set; }
        public User Owner { get; set; }
        public Statuses Status { get; set; }
    }
}