using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WidePictBoard.Domain.General;

namespace WidePictBoard.Domain
{
    public class Comment : EntityMutable<int>
    {
        [MaxLength(2048)]
        public string Body { get; set; }
        public string OwnerId { get; set; }
        public User Owner { get; set; }
        public int ContentId { get; set; }
        public Content Content { get; set; }
        public int? ParentCommentId { get; set; }
        public Comment ParentComment { get; set; }
        public virtual ICollection<Comment> ChildComments { get; set; }
    }
}