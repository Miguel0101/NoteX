using NoteX.Domain.Common.Interfaces;
using NoteX.Domain.Users.ValueObjects;

namespace NoteX.Domain.Users.Events;

public record UserNameUpdatedDomainEvent(Guid UserId, Name Name) : IDomainEvent
{
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}