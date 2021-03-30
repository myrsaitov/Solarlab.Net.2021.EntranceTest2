using WidePictBoard.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WidePictBoard.Infrastructure.DataAccess.EntitiesConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.UpdatedAt).IsRequired(false);
            builder.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode();
            builder.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode();
            builder.Property(x => x.MiddleName)
                .HasMaxLength(100)
                .IsUnicode();
        }
    }
}