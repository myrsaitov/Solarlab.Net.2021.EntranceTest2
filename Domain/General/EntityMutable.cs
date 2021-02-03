using System;

namespace WidePictBoard.Domain.General
{
    public class EntityMutable<TId> : EntityTimed<TId>
    {
        public DateTime UpdateOn { get; set; }
    }
}