using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FakeAcad.Core.Entities;

namespace FakeAcad.Infrastructure.EntityConfigurations;

public class ArticleConfigurations  : IEntityTypeConfiguration<Article>
{
    public void Configure(EntityTypeBuilder<Article> builder)
    {
        builder.Property(e => e.Id) // This specifies which property is configured.
            .IsRequired(); // Here it is specified if the property is required, meaning it cannot be null in the database.
        builder.HasKey(x => x.Id); // Here it is specified that the property Id is the primary key.
        builder.Property(e => e.Title)
            .HasMaxLength(255) // This specifies the maximum length for varchar type in the database.
            .IsRequired();
        builder.HasAlternateKey(e => e.Title); // Here it is specified that the property Email is a unique key.
        builder.Property(e => e.Description)
            .HasMaxLength(1024)
            .IsRequired();
        builder.Property(e => e.Content)
            .HasMaxLength(4096)
            .IsRequired();
        builder.Property(e => e.CreatedAt)
            .IsRequired();
        builder.Property(e => e.UpdatedAt)
            .IsRequired();
    }
}