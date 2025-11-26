using NoteX.Domain.Common.Entities;
using NoteX.Domain.Notes.Entities;
using NoteX.Domain.Users.Events;
using NoteX.Domain.Users.Exceptions;
using NoteX.Domain.Users.ValueObjects;

namespace NoteX.Domain.Users.Entities;

/// <summary>
/// User - Domain Aggregate
/// <para>Represents a user with name, email and password.</para>
/// <para>A user can generate and verify an account verification code.</para>
/// </summary>
public class User : AggregateRoot
{
    private readonly List<VerificationCode> _verificationCodes = [];

    public Guid Id { get; private set; }
    public Name Name { get; private set; }
    public Email Email { get; private set; }
    public Password Password { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public IReadOnlyCollection<VerificationCode> VerificationCodes => _verificationCodes.AsReadOnly();
    public ICollection<Note> Notes { get; } = [];

    private User(Name name, Email email, Password password)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        Password = password;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = null;
    }

    /// <summary>
    /// Register a user with name, email and password.
    /// </summary>
    /// <param name="name">The name</param>
    /// <param name="email">The email</param>
    /// <param name="password">The password</param>
    /// <returns>The registered user.</returns>
    public static User Register(Name name, Email email, Password password)
    {
        User user = new(name, email, password);

        user.AddDomainEvent(new UserRegisteredDomainEvent(user.Id, name, email));

        return user;
    }

    /// <summary>
    /// Updates the name of a existing user.
    /// </summary>
    /// <param name="name">The updated name</param>
    /// <returns>The updated user.</returns>
    public User UpdateName(Name name)
    {
        Name = name;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new UserNameUpdatedDomainEvent(Id, name));

        return this;
    }

    /// <summary>
    /// Updates the email of a existing user.
    /// </summary>
    /// <param name="email">The updated email</param>
    /// <returns>The updated user.</returns>
    public User UpdateEmail(Email email)
    {
        Email = email;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new UserEmailUpdatedDomainEvent(Id, email));

        return this;
    }

    /// <summary>
    /// Generates an account verification code.
    /// </summary>
    /// <returns>The generated verification code.</returns>
    /// <exception cref="VerificationCodePendingException">
    /// Thrown when there is already a pending verification code.
    /// </exception>
    public VerificationCode GenerateVerificationCode()
    {
        if (_verificationCodes.Any(v => v.IsPending()))
        {
            throw new VerificationCodePendingException();
        }

        VerificationCode code = new(Id);

        _verificationCodes.Add(code);

        AddDomainEvent(new UserVerificationCodeGeneratedDomainEvent(Id, Name, Email, code.Code));

        return code;
    }

    /// <summary>
    /// Checks an account verification code.
    /// </summary>
    /// <param name="code">The code to be verified</param>
    /// <returns></returns>
    /// <exception cref="VerificationCodeNotFoundException">
    /// Thrown when the code cannot be found.
    /// </exception>
    public VerificationCode VerifyVerificationCode(Code code)
    {
        VerificationCode verificationCode = _verificationCodes
            .FirstOrDefault(v => v.Code == code && v.IsPending())
            ?? throw new VerificationCodeNotFoundException();

        verificationCode.Verify();

        return verificationCode;
    }
}