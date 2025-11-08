namespace NoteX.Domain.Notes.ValueObjects;

public record Title
{
    public const int MinLength = 5;
    public const int MaxLength = 100;

    public string Value { get; } = string.Empty;

    private Title(string title)
    {
        Value = title;
    }

    public static Title Create() { }
}