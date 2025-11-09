namespace NoteX.Domain.Notes.Exceptions;

public class ContentOutOfRangeException(int MaxLength) : Exception($"The content must be a maximum of {MaxLength} characters.");