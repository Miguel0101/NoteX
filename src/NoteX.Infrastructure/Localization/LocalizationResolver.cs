using NoteX.Application.Common.Interfaces;

namespace NoteX.Infrastructure.Localization;

public class LocalizationResolver : ILocalizationResolver
{
    public string? GetLanguage(string? languageHeaderContent, List<string> supportedLanguages)
    {
        if (string.IsNullOrWhiteSpace(languageHeaderContent)) return null;

        return supportedLanguages.FirstOrDefault(l => l.Contains(languageHeaderContent));
    }
}