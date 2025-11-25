using Microsoft.AspNetCore.Identity;
using NoteX.Domain.Users.Exceptions;

namespace NoteX.Domain.Users.ValueObjects;

public record Password
{
    public const int MinLength = 8;
    public const int MaxLength = 128;

    private static readonly PasswordHasher<string?> Hasher = new();

    public string HashedValue { get; } = string.Empty;

    private Password(string hashPassword)
    {
        HashedValue = hashPassword;
    }

    public static Password Create(string? password)
    {
        if (password == null)
        {
            throw new PasswordNullException();
        }

        if (string.IsNullOrWhiteSpace(password))
        {
            throw new PasswordEmptyException();
        }

        if (password.Length < MinLength || password.Length > MaxLength)
        {
            throw new PasswordOutOfRangeException(MinLength, MaxLength);
        }

        string hashed = Hasher.HashPassword(null, password);

        return new Password(hashed);
    }

    public static Password FromHash(string hashedValue)
    {
        return new Password(hashedValue);
    }

    public bool Verify(string? password)
    {
        return Hasher.VerifyHashedPassword(null, HashedValue, password) != PasswordVerificationResult.Failed;
    }
}