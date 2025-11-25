using Microsoft.AspNetCore.Mvc;
using NoteX.Application.Common.Results;
using NoteX.Application.Common.Results.Enums;

namespace NoteX.API.Mappers;

public static class ResultMapper
{
    public static IActionResult ToActionResult(this Result result) => result.Code switch
    {
        // Success
        ResultCode.Success => new OkObjectResult(result),
        ResultCode.SignInSuccessfully => new OkObjectResult(result),
        ResultCode.SignUpSuccessfully => new CreatedResult(string.Empty, result),
        ResultCode.AccountVerificationCodeSent => new OkObjectResult(result),
        ResultCode.VerificationCodeVerifiedError => new OkObjectResult(result),

        // Account
        ResultCode.AccountNotFoundError => new NotFoundObjectResult(result),
        ResultCode.AccountAlreadyExistsError => new ConflictObjectResult(result),
        ResultCode.InvalidPasswordError => new UnauthorizedObjectResult(result),

        // Email
        ResultCode.EmailNullError => new BadRequestObjectResult(result),
        ResultCode.EmailEmptyError => new BadRequestObjectResult(result),
        ResultCode.EmailFormatError => new BadRequestObjectResult(result),

        // Name
        ResultCode.NameNullError => new BadRequestObjectResult(result),
        ResultCode.NameEmptyError => new BadRequestObjectResult(result),
        ResultCode.NameOutOfRangeError => new BadRequestObjectResult(result),

        // Password
        ResultCode.PasswordNullError => new BadRequestObjectResult(result),
        ResultCode.PasswordEmptyError => new BadRequestObjectResult(result),
        ResultCode.PasswordOutOfRangeError => new BadRequestObjectResult(result),

        // Verification Code
        ResultCode.VerificationCodeNotFoundError => new NotFoundObjectResult(result),
        ResultCode.VerificationCodeExpiredError => new NotFoundObjectResult(result),
        ResultCode.VerificationCodePendingError => new ConflictObjectResult(result),

        // Note
        ResultCode.NoteNotFoundError => new NotFoundObjectResult(result),
        ResultCode.NoteAlreadyExistsError => new ConflictObjectResult(result),

        // Title
        ResultCode.TitleNullError => new BadRequestObjectResult(result),
        ResultCode.TitleEmptyError => new BadRequestObjectResult(result),
        ResultCode.TitleOutOfRangeError => new BadRequestObjectResult(result),

        // Content
        ResultCode.ContentNullError => new BadRequestObjectResult(result),
        ResultCode.ContentOutOfRangeError => new BadRequestObjectResult(result),

        // Internal Error
        ResultCode.InternalError => new ObjectResult(result) { StatusCode = 500 },

        // Default
        _ => new BadRequestObjectResult(result)
    };
}
