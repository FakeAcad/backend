using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Infrastructure.EntityConfigurations;

public class ProfessorConfigurations : IEntityTypeConfiguration<Professor>
{
    public void Configure(EntityTypeBuilder<Professor> builder)
    {
        builder.Property(e => e.Id) // This specifies which property is configured.
            .IsRequired(); // Here it is specified if the property is required, meaning it cannot be null in the database.
        builder.HasKey(x => x.Id); // Here it is specified that the property Id is the primary key.
        builder.Property(e => e.FirstName)
            .HasMaxLength(255) // This specifies the maximum length for varchar type in the database.
            .IsRequired();
        builder.Property(e => e.LastName)
            .HasMaxLength(255) // This specifies the maximum length for varchar type in the database.
            .IsRequired();
        builder.Property(e => e.Position)
            .HasConversion(new EnumToStringConverter<Position>())
            .HasMaxLength(255)
            .IsRequired();
        builder.Property(e => e.CreatedAt)
            .IsRequired();
        builder.Property(e => e.UpdatedAt)
            .IsRequired();
    }
}