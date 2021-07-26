using SL2021.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SL2021.Infrastructure.DataAccess.EntitiesConfiguration
{
    public class ContentConfiguration : IEntityTypeConfiguration<Content>
    {
        public void Configure(EntityTypeBuilder<Content> builder)
        {
            builder.HasKey(con => con.Id);
            builder.Property(con => con.CreatedAt).IsRequired();
            builder.Property(con => con.UpdatedAt).IsRequired(false);
            builder.HasMany(con => con.Tags)
                .WithMany(t => t.Contents)
                .UsingEntity(j => j.ToTable("TagContent"));
        }
    }
}