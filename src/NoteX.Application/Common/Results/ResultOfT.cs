using NoteX.Application.Common.Results.Enums;

namespace NoteX.Application.Common.Results;

public class Result<TData> : Result
{
    public TData Data { get; private set; }

    private Result(ResultCode code, TData data, params object[] args) : base(code, args)
    {
        Data = data;
    }

    public static Result<TData> Success(ResultCode code, TData data, params object[] args) => new(code, data, args);
}