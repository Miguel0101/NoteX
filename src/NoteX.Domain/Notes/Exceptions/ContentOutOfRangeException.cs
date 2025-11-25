namespace NoteX.Domain.Notes.Exceptions;

public class ContentOutOfRangeException(int maxLength) : Exception($"The content must be a maximum of {maxLength} characters.")
{
    public int MaxLength { get; } = maxLength;
}