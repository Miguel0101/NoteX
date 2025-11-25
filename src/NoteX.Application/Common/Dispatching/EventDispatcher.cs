using Microsoft.Extensions.DependencyInjection;
using NoteX.Domain.Common.Interfaces;

namespace NoteX.Application.Common.Dispatching;

public class EventDispatcher : IEventDispatcher
{
    private readonly IServiceProvider _provider;

    public EventDispatcher(IServiceProvider provider)
    {
        _provider = provider;
    }

    public async Task DispatchAsync(IDomainEvent @event, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(@event);

        var handlerType = typeof(IEventHandler<>).MakeGenericType(@event.GetType());

        var handlers = (IEnumerable<object>)_provider.GetServices(handlerType);

        foreach (var handler in handlers)
        {
            await ((dynamic)handler).HandleAsync((dynamic)@event, cancellationToken);
        }
    }
}
