using NoteX.Domain.Notes.Exceptions;

namespace NoteX.Domain.Notes.ValueObjects;

public record Title
{
    public const int MinLength = 5;
    public const int MaxLength = 100;

    public string Value { get; } = string.Empty;

    private Title(string title)
    {
        Value = title;
    }

    public static Title Create(string? title)
    {
        if (title == null)
        {
            throw new TitleNullException();
        }

        if (string.IsNullOrWhiteSpace(title))
        {
            throw new TitleEmptyException();
        }

        if (title.Length < MinLength || title.Length > MaxLength)
        {
            throw new TitleOutOfRangeException(MinLength, MaxLength);
        }

        return new Title(title);
    }
}