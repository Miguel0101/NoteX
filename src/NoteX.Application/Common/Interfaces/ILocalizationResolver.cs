namespace NoteX.Application.Common.Interfaces;

public interface ILocalizationResolver
{
    string? GetLanguage(string? languageHeaderContent, List<string> supportedLanguages);
}