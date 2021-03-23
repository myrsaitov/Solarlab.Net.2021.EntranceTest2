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
                Name = "Автомобили",
                CreatedAt = DateTime.UtcNow
            },
            new Category
            {
                Id = 2,
                Name = "Велосипеды",
                CreatedAt = DateTime.UtcNow

            }
            ,
            new Category
            {
                Id = 3,
                Name = "Самокаты",
                CreatedAt = DateTime.UtcNow
            }
        };

        ///<inheritdoc />>
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired();

            builder.Property(x => x.ParentCategoryId_).IsRequired(false);

            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.UpdatedAt).IsRequired(false);

            builder.HasData(categories);
        }
    }
}