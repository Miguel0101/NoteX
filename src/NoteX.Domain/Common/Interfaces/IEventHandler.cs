namespace NoteX.Domain.Common.Interfaces;

public interface IEventHandler<TDomainEvent> where TDomainEvent : IDomainEvent
{
    public Task HandleAsync(TDomainEvent @event, CancellationToken cancellationToken = default);
}