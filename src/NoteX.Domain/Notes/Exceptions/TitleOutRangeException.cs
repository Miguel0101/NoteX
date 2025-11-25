namespace NoteX.Domain.Notes.Exceptions;

public class TitleOutOfRangeException(int minLength, int maxLength) : Exception($"The title must be between {minLength} and {maxLength} characters.")
{
    public int MinLength { get; } = minLength;
    public int MaxLength { get; } = maxLength;
}