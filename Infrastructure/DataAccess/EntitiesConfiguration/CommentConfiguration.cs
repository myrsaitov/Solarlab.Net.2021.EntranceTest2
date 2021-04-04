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
            builder.HasKey(com => com.Id);
            builder.Property(com => com.CreatedAt).IsRequired();
            builder.Property(com => com.UpdatedAt).IsRequired(false);
            /*builder.HasOne<Content>()
                .WithMany()
                .HasForeignKey(com => com.ContentId);
            builder.HasOne(com => com.Owner)
                .WithMany()
                .HasForeignKey(com => com.OwnerId)
                .HasPrincipalKey(u => u.Id);*/
        }
    }
}