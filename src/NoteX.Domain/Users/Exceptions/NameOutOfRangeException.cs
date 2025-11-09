namespace NoteX.Domain.Users.Exceptions;

public class NameOutOfRangeException(int MinLength, int MaxLength) : Exception($"The name must be between {MinLength} and {MaxLength} characters.");