using System.Globalization;
using Microsoft.Extensions.Options;
using NoteX.API.Configurations;
using NoteX.Application.Common.Interfaces;

namespace NoteX.API.Middlewares;

public class TranslationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILocalizationResolver _localizationResolver;
    private readonly Localization _localizationConfiguration;

    public TranslationMiddleware(RequestDelegate next, ILocalizationResolver localizationResolver, IOptions<Localization> localizationConfiguration)
    {
        _next = next;
        _localizationResolver = localizationResolver;
        _localizationConfiguration = localizationConfiguration.Value;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        string? languageHeaderContent = context.Request.Headers["X-Language"];
        string language = _localizationResolver.GetLanguage(languageHeaderContent, _localizationConfiguration.SupportedLanguages) ?? _localizationConfiguration.DefaultLanguage;

        CultureInfo culture = new(language);

        CultureInfo.CurrentCulture = culture;
        CultureInfo.DefaultThreadCurrentCulture = culture;

        await _next(context);
    }
}