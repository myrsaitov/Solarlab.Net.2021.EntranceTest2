using System;
using System.Collections.Generic;
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
                CreatedAt = DateTime.UtcNow
            },
            new Category
            {
                Id = 2,
                Name = "Недвижимость",
                CreatedAt = DateTime.UtcNow

            },
            new Category
            {
                Id = 3,
                Name = "Мебель",
                CreatedAt = DateTime.UtcNow
            },
            new Category
            {
                Id = 4,
                Name = "Одежда",
                CreatedAt = DateTime.UtcNow
            },
            new Category
            {
                Id = 5,
                Name = "Бытовая техника",
                CreatedAt = DateTime.UtcNow
            },
            new Category
            {
                Id = 6,
                Name = "Книги",
                CreatedAt = DateTime.UtcNow
            }
        };

        ///<inheritdoc />>
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired();

            builder.Property(x => x.ParentCategoryId).IsRequired(false);

            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.UpdatedAt).IsRequired(false);

            builder.HasData(categories);
        }
    }
}