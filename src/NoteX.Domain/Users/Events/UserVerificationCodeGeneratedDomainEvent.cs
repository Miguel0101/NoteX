using NoteX.Domain.Common.Interfaces;
using NoteX.Domain.Users.ValueObjects;

namespace NoteX.Domain.Users.Events;

public record UserVerificationCodeGeneratedDomainEvent(Guid UserId, Name Name, Email Email, Code Code) : IDomainEvent
{
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}