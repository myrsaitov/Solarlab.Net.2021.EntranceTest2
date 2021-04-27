using System;
using SL2021.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SL2021.Infrastructure.DataAccess.EntitiesConfiguration
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Body).IsRequired();
            builder.Property(t => t.CreatedAt).IsRequired();
        }
    }
}