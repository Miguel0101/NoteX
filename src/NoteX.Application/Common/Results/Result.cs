using NoteX.Application.Common.Results.Enums;
using NoteX.Application.Common.Results.Helpers;

namespace NoteX.Application.Common.Results;

public class Result
{
    public ResultCode Code { get; private set; }
    public string Message { get; private set; }

    protected Result(ResultCode code, params object[] args)
    {
        Code = code;
        Message = code.GetMessage(args);
    }

    public static Result Success(ResultCode code, params object[] args) => new(code, args);
    public static Result Failure(ResultCode code, params object[] args) => new(code, args);
}