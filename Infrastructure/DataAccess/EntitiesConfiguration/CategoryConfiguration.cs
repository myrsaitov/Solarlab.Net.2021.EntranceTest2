using System;
using WidePictBoard.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WidePictBoard.Infrastructure.DataAccess.EntitiesConfiguration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        private Category[] categories = new Category[]
        {
            new Category
            {
                Id = 1,
                Name = "Транспорт",
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            },
            new Category
            {
                Id = 2,
                Name = "Недвижимость",
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false

            },
            new Category
            {
                Id = 3,
                Name = "Мебель",
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            },
            new Category
            {
                Id = 4,
                Name = "Одежда",
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            },
            new Category
            {
                Id = 5,
                Name = "Бытовая техника",
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            },
            new Category
            {
                Id = 6,
                Name = "Книги",
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            }
        };

        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(cat => cat.Id);
            builder.Property(cat => cat.Name).IsRequired();
            builder.Property(cat => cat.ParentCategoryId).IsRequired(false);
            builder.Property(cat => cat.CreatedAt).IsRequired();
            builder.Property(cat => cat.UpdatedAt).IsRequired(false);
            builder.HasData(categories);
            builder.HasOne(cat => cat.ParentCategory)
                .WithMany()
                .HasForeignKey(cat => cat.ParentCategoryId);
        }
    }
}