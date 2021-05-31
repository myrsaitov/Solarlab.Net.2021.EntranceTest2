using SL2021.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SL2021.Infrastructure.DataAccess.EntitiesConfiguration
{
    public class WebLinkConfiguration : IEntityTypeConfiguration<WebLink>
    {
        public void Configure(EntityTypeBuilder<WebLink> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.CreatedAt).IsRequired();
        }
    }
}