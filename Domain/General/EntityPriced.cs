using System;

namespace WidePictBoard.Domain.General
{
    public abstract class EntityPriced<TId> : EntityMutable<TId>
    {
        public string CreatorId { get; set; }
        public decimal Price { get; set; }
    }
}