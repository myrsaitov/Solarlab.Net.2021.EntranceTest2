using System;

namespace WidePictBoard.Domain.General
{
    public abstract class EntityTimed<TId> : Entity<TId>
    {
        public DateTime CreatedOn { get; set; }
    }
}