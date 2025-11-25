namespace NoteX.Domain.Users.Exceptions;

public class NameOutOfRangeException(int minLength, int maxLength) : Exception($"The name must be between {minLength} and {maxLength} characters.")
{
    public int MinLength { get; } = minLength;
    public int MaxLength { get; } = maxLength;
}