using System.Net.Mail;
using NoteX.Domain.Users.Exceptions;

namespace NoteX.Domain.Users.ValueObjects;

public record Email
{
    public string Value { get; } = string.Empty;

    private Email(string email)
    {
        Value = email;
    }

    public static Email Create(string? email)
    {
        if (email == null)
        {
            throw new EmailNullException();
        }

        if (string.IsNullOrWhiteSpace(email))
        {
            throw new EmailEmptyException();
        }

        try
        {
            var mail = new MailAddress(email);

            return new Email(email);
        }
        catch
        {
            throw new EmailFormatException();
        }
    }
}