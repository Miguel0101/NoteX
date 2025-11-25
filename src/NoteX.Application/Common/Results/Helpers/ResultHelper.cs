using NoteX.Application.Common.Results.Enums;
using NoteX.Application.Common.Results.Resources;

namespace NoteX.Application.Common.Results.Helpers;

public static class ResultMapper
{
    public static string GetMessage(this ResultCode code, params object[] args)
    {
        var template = ResultMessages.Get(code.ToString());

        if (args == null || args.Length == 0)
            return template;

        return string.Format(template, args);
    }
}