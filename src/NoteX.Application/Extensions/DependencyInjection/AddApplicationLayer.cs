using Microsoft.Extensions.DependencyInjection;
using NoteX.Application.Common.Dispatching;
using NoteX.Application.Notes.Services;
using NoteX.Application.Users.Handlers;
using NoteX.Application.Users.Services;
using NoteX.Domain.Common.Interfaces;
using NoteX.Domain.Users.Events;

namespace NoteX.Application.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddScoped<IEventDispatcher, EventDispatcher>();
        services.AddScoped<IEventHandler<UserVerificationCodeGeneratedDomainEvent>, UserVerificationCodeGeneratedHandler>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<INoteService, NoteService>();

        return services;
    }
}