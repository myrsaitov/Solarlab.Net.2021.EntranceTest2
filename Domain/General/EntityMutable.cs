using System;

namespace WidePictBoard.Domain.General
{
    public class EntityMutable<TId> : Entity<TId>
    {
        public DateTime? UpdatedAt { get; set; }
    }
}