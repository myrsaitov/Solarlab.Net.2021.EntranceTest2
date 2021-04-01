using System;
using WidePictBoard.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Org.BouncyCastle.Math.EC.Rfc7748;


namespace WidePictBoard.Infrastructure.DataAccess.EntitiesConfiguration
{
    public class ContentConfiguration : IEntityTypeConfiguration<Content>
    {
        public void Configure(EntityTypeBuilder<Content> builder)
        {
            builder.HasKey(con => con.Id);
            builder.Property(con => con.CreatedAt).IsRequired();
            builder.Property(con => con.UpdatedAt).IsRequired(false);
            builder.Property(con => con.Price).HasColumnType("money");
            builder.HasOne(con => con.Category)
                .WithMany(cat => cat.Contents)
                .HasForeignKey(con => con.CategoryId)
                .HasPrincipalKey(cat => cat.Id);
            builder.HasMany(con => con.Tags)
                .WithMany(t => t.Contents)
                .UsingEntity(
                    j => j
                        .HasOne(pt => pt.Tag)
                        .WithMany(t => t.)
                    
                    );
            builder.HasOne(con => con.Owner)
                .WithMany()
                .HasForeignKey(con => con.OwnerId)
                .HasPrincipalKey(u => u.Id);
        }
    }
}