using NoteX.Domain.Common.Interfaces;

namespace NoteX.Application.Common.Dispatching;

public interface IEventDispatcher
{
    public Task DispatchAsync(IDomainEvent @event, CancellationToken cancellationToken = default);
}