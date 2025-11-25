using NoteX.Domain.Notes.Exceptions;

namespace NoteX.Domain.Notes.ValueObjects;

public record Content
{
    public const int MaxLength = 3000;
    
    public string Value { get; } = string.Empty;

    private Content(string content)
    {
        Value = content;
    }

    public static Content Create(string? content)
    {
        if (content == null)
        {
            throw new ContentNullException();
        }

        if (content.Length > MaxLength)
        {
            throw new ContentOutOfRangeException(MaxLength);
        }

        return new Content(content);
    }
}