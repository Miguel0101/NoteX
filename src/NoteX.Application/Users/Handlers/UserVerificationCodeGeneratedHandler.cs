using NoteX.Domain.Common.Interfaces;
using NoteX.Domain.Users.Events;

namespace NoteX.Application.Users.Handlers;

public class UserVerificationCodeGeneratedHandler : IEventHandler<UserVerificationCodeGeneratedDomainEvent>
{
    public Task HandleAsync(UserVerificationCodeGeneratedDomainEvent @event, CancellationToken cancellationToken = default)
    {
        Console.WriteLine($"[{@event.OccurredOn:d}] Código gerado para {@event.Name.Value} no email {@event.Email.Value}: {@event.Code.Value}");
        return Task.CompletedTask;
    }
}