using System;
using WidePictBoard.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WidePictBoard.Infrastructure.DataAccess.EntitiesConfiguration
{
    public class TagContentConfiguration : IEntityTypeConfiguration<TagContent>
    {
        public void Configure(EntityTypeBuilder<TagContent> builder)
        {
            /* builder.HasKey(cont => new{cont.ContentId,cont.TagId});
            builder.HasOne(cont => cont.Content)
                .WithMany(con => con.TagContents)
                .HasForeignKey(cont => cont.TagId);
            builder.HasOne(cont => cont.Tag)
                .WithMany(t => t.TagContents)
                .HasForeignKey(cont => cont.ContentId);*/
        }
    }
}