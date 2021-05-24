using SL2021.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SL2021.Infrastructure.DataAccess.EntitiesConfiguration
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(com => com.Id);
            builder.Property(com => com.CreatedAt).IsRequired();
            builder.Property(com => com.UpdatedAt).IsRequired(false);
        }
    }
}