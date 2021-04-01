using System;
using WidePictBoard.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WidePictBoard.Infrastructure.DataAccess.EntitiesConfiguration
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.CreatedAt).IsRequired();
            builder.Property(p => p.UpdatedAt).IsRequired(false);
            builder.HasOne(p => p.Content)
                .WithMany()
                .HasForeignKey(s => s.ContentId)
                .HasPrincipalKey(u => u.Id);
            builder.HasOne(p => p.Owner)
                .WithMany()
                .HasForeignKey(s => s.OwnerId)
                .HasPrincipalKey(u => u.Id);
        }
    }
}