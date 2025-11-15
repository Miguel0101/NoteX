using System.Security.Cryptography;

namespace NoteX.Domain.Users.ValueObjects;

public record Code
{
    public const int Digits = 8;

    public string Value { get; } = string.Empty;

    private Code(string code)
    {
        Value = code;
    }

    public static Code Create()
    {
        int max = (int)Math.Pow(10, Digits);

        string code = RandomNumberGenerator
            .GetInt32(max)
            .ToString($"D{Digits}");

        return new Code(code);
    }
}