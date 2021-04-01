using System;
using WidePictBoard.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WidePictBoard.Infrastructure.DataAccess.EntitiesConfiguration
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        private Tag[] tags = new Tag[]
        {
            new Tag
            {
                Id = 1,
                Body = "tag1",
                CreatedAt = DateTime.UtcNow
            },
            new Tag
            {
                Id = 2,
                Body = "tag2",
                CreatedAt = DateTime.UtcNow
            },
            new Tag
            {
                Id = 3,
                Body = "tag3",
                CreatedAt = DateTime.UtcNow
            },
            new Tag
            {
                Id = 4,
                Body = "tag4",
                CreatedAt = DateTime.UtcNow
            },
            new Tag
            {
                Id = 5,
                Body = "tag5",
                CreatedAt = DateTime.UtcNow
            },
            new Tag
            {
                Id = 6,
                Body = "tag6",
                CreatedAt = DateTime.UtcNow
            }
        };
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Body).IsRequired();
            builder.Property(t => t.CreatedAt).IsRequired();
            builder.Property(t => t.UpdatedAt).IsRequired(false);
        }
    }
}