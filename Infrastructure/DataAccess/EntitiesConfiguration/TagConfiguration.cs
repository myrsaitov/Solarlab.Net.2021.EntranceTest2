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
                Body = "машины",
                CreatedAt = DateTime.UtcNow
            },
            new Tag
            {
                Id = 2,
                Body = "квартиры",
                CreatedAt = DateTime.UtcNow

            },
            new Tag
            {
                Id = 3,
                Body = "техника",
                CreatedAt = DateTime.UtcNow
            },
            new Tag
            {
                Id = 4,
                Body = "кисти",
                CreatedAt = DateTime.UtcNow
            },
            new Tag
            {
                Id = 5,
                Body = "краски",
                CreatedAt = DateTime.UtcNow
            },
            new Tag
            {
                Id = 6,
                Body = "Книги",
                CreatedAt = DateTime.UtcNow
            }
        };
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Body).IsRequired();
            builder.Property(t => t.CreatedAt).IsRequired();
            builder.Property(t => t.UpdatedAt).IsRequired(false);
            builder.HasData(tags);
        }
    }
}