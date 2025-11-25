using NoteX.API.Middlewares;

namespace NoteX.API.Extensions.Middleware;

public static class TranslationMiddlewareExtensions
{
    public static IApplicationBuilder UseTranslation(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<TranslationMiddleware>();

        return builder;
    }
}