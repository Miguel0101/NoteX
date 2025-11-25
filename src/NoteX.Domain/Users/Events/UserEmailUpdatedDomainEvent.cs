using NoteX.Domain.Common.Interfaces;
using NoteX.Domain.Users.ValueObjects;

namespace NoteX.Domain.Users.Events;

public record UserEmailUpdatedDomainEvent(Guid UserId, Email Email) : IDomainEvent
{
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}