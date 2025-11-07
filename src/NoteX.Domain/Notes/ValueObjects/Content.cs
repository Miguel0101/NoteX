using NoteX.Domain.Notes.Exceptions;

namespace NoteX.Domain.Notes.ValueObjects;

public record Content
{
    public const int MaxLenght = 3000;
    
    public string Value { get; } = string.Empty;

    private Content(string content)
    {
        Value = content;
    }

    public static Content Create(string content)
    {
        if (content == null)
        {
            throw new ContentNullException();
        }

        if (content.Length > MaxLenght)
        {
            throw new ContentOutOfRangeException(MaxLenght);
        }

        return new Content(content);
    }

    public static void Validate(Content content)
    {
        if (content == null)
        {
            throw new ContentNullException();
        }
    }
}