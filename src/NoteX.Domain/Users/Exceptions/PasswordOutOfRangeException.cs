namespace NoteX.Domain.Users.Exceptions;

public class PasswordOutOfRangeException(int MinLength, int MaxLength) : Exception($"The password must be between {MinLength} and {MaxLength} characters.");