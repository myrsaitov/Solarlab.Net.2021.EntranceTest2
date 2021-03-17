using System;
using WidePictBoard.Domain.General;

namespace WidePictBoard.Domain
{
    public class Comment : EntityMutable<int>
    {
        public string Body { get; set; }

        public DateTime CategoryDate { get; set; }
    }
}