using NoteX.Domain.Common.Entities;
using NoteX.Domain.Notes.Entities;
using NoteX.Domain.Users.Events;
using NoteX.Domain.Users.Exceptions;
using NoteX.Domain.Users.ValueObjects;

namespace NoteX.Domain.Users.Entities;

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

    public static User Register(Name name, Email email, Password password)
    {
        User user = new(name, email, password);

        user.AddDomainEvent(new UserRegisteredDomainEvent(user.Id, name, email));

        return user;
    }

    public User UpdateName(Name name)
    {
        Name = name;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new UserNameUpdatedDomainEvent(Id, name));

        return this;
    }

    public User UpdateEmail(Email email)
    {
        Email = email;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new UserEmailUpdatedDomainEvent(Id, email));

        return this;
    }

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

    public VerificationCode VerifyVerificationCode(Code code)
    {
        VerificationCode verificationCode = _verificationCodes
            .FirstOrDefault(v => v.Code == code && v.IsPending())
            ?? throw new VerificationCodeNotFoundException();

        verificationCode.Verify();

        return verificationCode;
    }
}