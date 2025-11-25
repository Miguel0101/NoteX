using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NoteX.Domain.Notes.Entities;
using NoteX.Domain.Notes.ValueObjects;

namespace NoteX.Infrastructure.Notes.Configurations;

public class NoteConfiguration : IEntityTypeConfiguration<Note>
{
    public void Configure(EntityTypeBuilder<Note> builder)
    {
        builder.HasKey(n => n.Id);

        builder.Property(n => n.Id)
           .ValueGeneratedNever()
           .IsRequired();

        builder.Property(n => n.Title)
            .HasConversion(title => title.Value, value => Title.Create(value))
            .HasColumnName("Title")
            .HasMaxLength(Title.MaxLength)
            .IsRequired();

        builder.Property(n => n.Content)
            .HasConversion(content => content.Value, value => Content.Create(value))
            .HasColumnName("Content")
            .HasMaxLength(Content.MaxLength)
            .IsRequired();
    }
}