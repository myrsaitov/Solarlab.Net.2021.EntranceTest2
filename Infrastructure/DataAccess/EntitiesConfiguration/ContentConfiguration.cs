using System;
using WidePictBoard.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WidePictBoard.Infrastructure.DataAccess.EntitiesConfiguration
{
    public class ContentConfiguration : IEntityTypeConfiguration<Content>
    {
        public void Configure(EntityTypeBuilder<Content> builder)
        {
            builder.HasKey(x => x.Id);
                
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.UpdatedAt).IsRequired(false);
                
            builder.Property(x => x.FirstName).HasMaxLength(100).IsUnicode();
            builder.Property(x => x.LastName).HasMaxLength(100).IsUnicode();
            builder.Property(x => x.Price).HasColumnType("money");

            builder.Property(x => x.Status)
                .HasConversion<string>(s => s.ToString(), s => Enum.Parse<Content.Statuses>(s));

            builder.HasOne(x => x.Owner)
                .WithMany()
                .HasForeignKey(s => s.OwnerId)
                .HasPrincipalKey(u => u.Id);
        }
    }
}