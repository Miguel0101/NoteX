namespace NoteX.Domain.Notes.Exceptions;

public class TitleOutOfRangeException(int MinLength, int MaxLength) : Exception($"The title must be between {MinLength} and {MaxLength} characters.");