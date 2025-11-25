using System.Globalization;
using System.Resources;

namespace NoteX.Application.Common.Results.Resources;

public static class ResultMessages
{
    private static readonly ResourceManager _resourceManager = new("NoteX.Application.Common.Results.Resources.ResultMessages", typeof(ResultMessages).Assembly);

    public static string Get(string key, CultureInfo? culture = null) =>
        _resourceManager.GetString(key, culture ?? CultureInfo.CurrentCulture) ?? $"Undefined error code: {key}";

}