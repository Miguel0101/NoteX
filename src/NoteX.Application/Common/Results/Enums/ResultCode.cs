namespace NoteX.Application.Common.Results.Enums;

public enum ResultCode
{
    // General
    Success,
    InternalError,

    // Auth
    SignInSuccessfully,
    SignUpSuccessfully,
    AccountVerificationCodeSent,
    AccountNotFoundError,
    AccountAlreadyExistsError,
    InvalidPasswordError,

    // Email
    EmailNullError,
    EmailEmptyError,
    EmailFormatError,

    // Name
    NameNullError,
    NameEmptyError,
    NameOutOfRangeError,

    // Password
    PasswordNullError,
    PasswordEmptyError,
    PasswordOutOfRangeError,

    // Verification Code
    VerificationCodeNotFoundError,
    VerificationCodeExpiredError,
    VerificationCodePendingError,
    VerificationCodeVerifiedError,

    // Note
    NoteNotFoundError,
    NoteAlreadyExistsError,

    // Title
    TitleNullError,
    TitleEmptyError,
    TitleOutOfRangeError,

    // Content
    ContentNullError,
    ContentOutOfRangeError
}