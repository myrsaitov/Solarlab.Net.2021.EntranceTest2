﻿using System.Collections.Generic;
using WidePictBoard.Domain.General;

namespace WidePictBoard.Domain
{
    public class Content : EntityMutable<int>
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public decimal Price { get; set; }
        public string OwnerId { get; set; }
        public User Owner { get; set; }
        public ContentStatus Status { get; set; }
        public Category Category { get; set; }
        public int? CategoryId { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}