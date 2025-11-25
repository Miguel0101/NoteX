namespace NoteX.Domain.Users.Exceptions;

public class PasswordOutOfRangeException(int minLength, int maxLength) : Exception($"The password must be between {minLength} and {maxLength} characters.")
{
    public int MinLength { get; } = minLength;
    public int MaxLength { get; } = maxLength;
}