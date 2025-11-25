using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NoteX.Domain.Users.Entities;
using NoteX.Domain.Users.ValueObjects;

namespace NoteX.Infrastructure.Users.Configurations;

public class VerificationCodeConfiguration : IEntityTypeConfiguration<VerificationCode>
{
    public void Configure(EntityTypeBuilder<VerificationCode> builder)
    {
        builder.ToTable("VerificationCodes");

        builder.HasKey(v => v.Id);

        builder.Property(v => v.Id)
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(v => v.Code)
            .HasConversion(code => code.Value, value => Code.FromCode(value))
            .HasColumnName("Code")
            .HasMaxLength(Code.Digits)
            .IsRequired();
    }
}