using SL2021.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SL2021.Infrastructure.DataAccess.EntitiesConfiguration
{
    public class UserPicConfiguration : IEntityTypeConfiguration<UserPic>
    {
        public void Configure(EntityTypeBuilder<UserPic> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.CreatedAt).IsRequired();
        }
    }
}