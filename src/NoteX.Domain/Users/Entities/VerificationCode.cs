using NoteX.Domain.Users.Exceptions;
using NoteX.Domain.Users.ValueObjects;

namespace NoteX.Domain.Users.Entities;

public class VerificationCode
{
    public Ulid Id { get; private set; }
    public Ulid UserId { get; private set; }
    public Code Code { get; private set; }
    public DateTime? VerifiedAt { get; private set; }
    public DateTime ExpiredAt { get; private set; }
    public DateTime CreatedAt { get; private set; }

    internal VerificationCode(Ulid userId)
    {
        Id = Ulid.NewUlid();
        UserId = userId;
        Code = Code.Create();
        VerifiedAt = null;
        CreatedAt = DateTime.UtcNow;
        ExpiredAt = CreatedAt.AddMinutes(5);
    }

    public bool IsExpired() => DateTime.UtcNow > ExpiredAt;
    public bool IsVerified() => VerifiedAt != null;
    public bool IsPending() => !IsExpired() && !IsVerified();

    internal void Verify()
    {
        if (IsExpired())
        {
            throw new VerificationCodeExpiredException();
        }

        if (IsVerified())
        {
            throw new VerificationCodeVerifiedException();
        }

        VerifiedAt = DateTime.UtcNow;
    }
}