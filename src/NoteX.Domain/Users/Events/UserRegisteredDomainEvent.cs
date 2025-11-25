using NoteX.Domain.Common.Interfaces;
using NoteX.Domain.Users.ValueObjects;

namespace NoteX.Domain.Users.Events;

public record UserRegisteredDomainEvent(Guid UserId, Name Name, Email Email) : IDomainEvent
{
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}