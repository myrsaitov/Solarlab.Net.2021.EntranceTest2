using System;
using System.ComponentModel.DataAnnotations;
using WidePictBoard.Domain.General;

namespace WidePictBoard.Domain
{
    public class Comment : EntityMutable<int>
    {
        [MaxLength(2048)]
        public string Body { get; set; }

        public DateTime CommentDate { get; set; }
        public CommentStatus Status { get; set; }
    }
}