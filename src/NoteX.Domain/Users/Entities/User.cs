using NoteX.Domain.Users.Exceptions;
using NoteX.Domain.Users.ValueObjects;

namespace NoteX.Domain.Users.Entities;

public class User
{
    private readonly List<VerificationCode> _verificationCodes = [];

    public Ulid Id { get; private set; }
    public Name Name { get; private set; }
    public Email Email { get; private set; }
    public Password Password { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public IReadOnlyCollection<VerificationCode> VerificationCodes => _verificationCodes.AsReadOnly();

    public User(Name name, Email email, Password password)
    {
        Name.Validate(name);
        Email.Validate(email);
        Password.Validate(password);

        Id = Ulid.NewUlid();
        Name = name;
        Email = email;
        Password = password;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = null;
    }

    public User UpdateName(Name name)
    {
        Name = name;
        UpdatedAt = DateTime.UtcNow;

        return this;
    }

    public User UpdateEmail(Email email)
    {
        Email = email;
        UpdatedAt = DateTime.UtcNow;

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