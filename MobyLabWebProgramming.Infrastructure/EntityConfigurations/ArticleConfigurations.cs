using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Infrastructure.EntityConfigurations;

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
        
        builder.HasOne(e => e.Complaint) // Aici se specifică o relație de unu-la-mulți.
            .WithOne(e => e.Article) // Aici se furnizează maparea inversă pentru relația de unu-la-mulți.
            .HasForeignKey<Article>(e => e.ComplaintId) // Aici este specificată coloana cheii străine.
            .HasPrincipalKey<Complaint>(e => e.ArticleId) // Aici se specifică cheia referențiată în tabela referențiată.
            .OnDelete(DeleteBehavior.Cascade); // Aici se specifică comportamentul de ștergere atunci când entitatea referențiată este eliminată.
    }
}