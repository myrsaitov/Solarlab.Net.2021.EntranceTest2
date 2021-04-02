using WidePictBoard.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
            builder.HasMany(con => con.Tags)
                .WithMany(t => t.Contents)
                .UsingEntity(j => j.ToTable("TagContent"));
            builder.HasOne(con => con.Category)
                .WithMany(cat => cat.Contents)
                .HasForeignKey(con => con.CategoryId)
                .HasPrincipalKey(cat => cat.Id);
            builder.HasOne(con => con.Owner)
                .WithMany()
                .HasForeignKey(con => con.OwnerId)
                .HasPrincipalKey(u => u.Id);
        }
    }
}