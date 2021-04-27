using System;

namespace SL2021.Domain.General
{
    public abstract class Entity<TId>
    {
        public TId Id { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}