using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SL2021.Domain.General;

namespace SL2021.Domain
{
    public class Comment : EntityMutable<int>
    {
        [MaxLength(2048)]
        public string Body { get; set; }
        public string OwnerId { get; set; }
        public virtual User Owner { get; set; }
        public int ContentId { get; set; }
        public virtual Content Content { get; set; }
        public int? ParentCommentId { get; set; }
        public virtual Comment ParentComment { get; set; }
        public virtual ICollection<Comment> ChildComments { get; set; }
    }
}