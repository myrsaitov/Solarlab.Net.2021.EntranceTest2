using System;

namespace SL2021.Domain.General
{
    public class EntityMutable<TId> : Entity<TId>
    {
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}