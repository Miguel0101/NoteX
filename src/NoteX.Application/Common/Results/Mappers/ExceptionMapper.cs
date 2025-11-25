using NoteX.Application.Common.Results.Enums;
using NoteX.Domain.Notes.Exceptions;
using NoteX.Domain.Users.Exceptions;

namespace NoteX.Application.Common.Results.Mappers;

public static class ExceptionMapper
{
    public static Result ToResult(this Exception ex) => ex switch
    {
        // Email
        EmailEmptyException => Result.Failure(ResultCode.EmailEmptyError),
        EmailFormatException => Result.Failure(ResultCode.EmailFormatError),
        EmailNullException => Result.Failure(ResultCode.EmailNullError),

        // Name
        NameEmptyException => Result.Failure(ResultCode.NameEmptyError),
        NameNullException => Result.Failure(ResultCode.NameNullError),
        NameOutOfRangeException e => Result.Failure(ResultCode.NameOutOfRangeError, e.MinLength, e.MaxLength),

        // Password
        PasswordEmptyException => Result.Failure(ResultCode.PasswordEmptyError),
        PasswordNullException => Result.Failure(ResultCode.PasswordNullError),
        PasswordOutOfRangeException e => Result.Failure(ResultCode.PasswordOutOfRangeError, e.MinLength, e.MaxLength),

        // Verification Code
        VerificationCodeExpiredException => Result.Failure(ResultCode.VerificationCodeExpiredError),
        VerificationCodeNotFoundException => Result.Failure(ResultCode.VerificationCodeNotFoundError),
        VerificationCodePendingException => Result.Failure(ResultCode.VerificationCodePendingError),
        VerificationCodeVerifiedException => Result.Failure(ResultCode.VerificationCodeVerifiedError),

        // Title
        TitleNullException => Result.Failure(ResultCode.TitleNullError),
        TitleEmptyException => Result.Failure(ResultCode.TitleEmptyError),
        TitleOutOfRangeException e => Result.Failure(ResultCode.TitleOutOfRangeError, e.MinLength, e.MaxLength),

        // Content
        ContentNullException => Result.Failure(ResultCode.ContentNullError),
        ContentOutOfRangeException e => Result.Failure(ResultCode.ContentOutOfRangeError, e.MaxLength),

        _ => Result.Failure(ResultCode.InternalError)
    };
}
