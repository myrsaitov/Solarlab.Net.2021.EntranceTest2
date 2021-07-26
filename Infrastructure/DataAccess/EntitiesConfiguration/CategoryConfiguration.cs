using System;
using SL2021.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SL2021.Infrastructure.DataAccess.EntitiesConfiguration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        private Category[] categories = new Category[]
        {
            new Category
            {
                Id = 1,
                Name = "Дни рождения",
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            },
            new Category
            {
                Id = 2,
                Name = "Дни свадьбы",
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false

            },
            new Category
            {
                Id = 3,
                Name = "Именины",
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            },
        };

        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(cat => cat.Id);
            builder.Property(cat => cat.Name).IsRequired();
            builder.Property(cat => cat.ParentCategoryId).IsRequired(false);
            builder.Property(cat => cat.CreatedAt).IsRequired();
            builder.Property(cat => cat.UpdatedAt).IsRequired(false);
            builder.HasData(categories);
        }
    }
}