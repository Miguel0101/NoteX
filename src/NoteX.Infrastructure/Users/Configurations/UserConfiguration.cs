using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NoteX.Domain.Users.Entities;
using NoteX.Domain.Users.ValueObjects;

namespace NoteX.Infrastructure.Users.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
           .ValueGeneratedNever()
           .IsRequired();

        builder.Property(u => u.Name)
            .HasConversion(name => name.Value, value => Name.Create(value))
            .HasColumnName("Name")
            .HasMaxLength(Name.MaxLength)
            .IsRequired();

        builder.Property(u => u.Email)
            .HasConversion(email => email.Value, value => Email.Create(value))
            .HasColumnName("Email")
            .IsRequired();

        builder.Property(u => u.Password)
            .HasConversion(password => password.HashedValue, hashed => Password.FromHash(hashed))
            .HasColumnName("Password")
            .IsRequired();

        builder.HasMany(u => u.VerificationCodes)
            .WithOne(v => v.User)
            .HasForeignKey(v => v.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}