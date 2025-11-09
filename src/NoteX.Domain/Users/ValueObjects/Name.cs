using NoteX.Domain.Users.Exceptions;

namespace NoteX.Domain.Users.ValueObjects;

public record Name
{
    public const int MinLength = 3;
    public const int MaxLength = 50;

    public string Value { get; } = string.Empty;

    private Name(string name)
    {
        Value = name;
    }

    public static Name Create(string name)
    {
        if (name == null)
        {
            throw new NameNullException();
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new NameEmptyException();
        }

        if (name.Length < MinLength || name.Length > MaxLength)
        {
            throw new NameOutOfRangeException(MinLength, MaxLength);
        }

        return new Name(name);
    }

    public static void Validate(Name name)
    {
        if (name == null)
        {
            throw new NameNullException();
        }
    }
}