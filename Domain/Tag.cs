using System.Collections.Generic;
using WidePictBoard.Domain.General;

namespace WidePictBoard.Domain
{
    public class Tag : EntityMutable<int>
    {
        public string TagText { get; set; }
    }
}