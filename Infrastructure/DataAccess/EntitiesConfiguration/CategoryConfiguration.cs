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
                Name = "Автомобили"
                // ,ChildCategories = new List<Category>()
                // {
                //     new Category() {Id = 3, Name = "Ауди",  ParentCategoryId = 1},
                //     new Category() {Id = 4, Name = "Жигули", ParentCategoryId = 1}
                // }
            },
            new Category
            {
                Id = 2,
                Name = "Усы"
            }
        };
        
        ///<inheritdoc />>
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id);
            
            builder.Property(x => x.CreatedAt).IsRequired();
            
            builder.HasData(categories);
        }
    }
}