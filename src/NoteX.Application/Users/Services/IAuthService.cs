using NoteX.Application.Common.Results;
using NoteX.Application.Users.DTOs.Requests;

namespace NoteX.Application.Users.Services;

public interface IAuthService
{
    Task<Result> LoginWithCredentialsAsync(LoginAccontRequest loginRequest);
    Task<Result> RegisterAccountAsync(RegisterAccountRequest registerRequest);
    Task<Result> VerifyAccountWithCodeAsync(SendAccountVerificationCodeRequest verificationCodeRequest);
    Task<Result> GetAccountDetailsAsync();
}