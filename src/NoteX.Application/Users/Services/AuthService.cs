using NoteX.Application.Common.Interfaces;
using NoteX.Application.Common.Results;
using NoteX.Application.Common.Results.Enums;
using NoteX.Application.Common.Results.Mappers;
using NoteX.Application.Users.DTOs.Requests;
using NoteX.Application.Users.DTOs.Responses;
using NoteX.Domain.Users.Entities;
using NoteX.Domain.Users.Interfaces;
using NoteX.Domain.Users.ValueObjects;

namespace NoteX.Application.Users.Services;

public class AuthService : IAuthService
{
    private readonly IJwtProvider _jwt;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;

    public AuthService(IJwtProvider jwt, IUserRepository userRepository, IUnitOfWork unitOfWork, IUserContext userContext)
    {
        _jwt = jwt;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _userContext = userContext;
    }

    public async Task<Result> LoginWithCredentialsAsync(LoginAccontRequest loginRequest)
    {
        try
        {
            Email email = Email.Create(loginRequest.Email);

            User? account = await _userRepository.GetByEmailAsync(email);

            if (account == null)
            {
                return Result.Failure(ResultCode.AccountNotFoundError);
            }

            if (!account.Password.Verify(loginRequest.Password))
            {
                return Result.Failure(ResultCode.InvalidPasswordError);
            }

            VerificationCode verificationCode = account.GenerateVerificationCode();
            await _unitOfWork.SaveChangesAsync();

            RequestAccountVerificationCodeResponse requestAccountVerification = new(email.Value);

            return Result<RequestAccountVerificationCodeResponse>.Success(ResultCode.AccountVerificationCodeSent, requestAccountVerification);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return e.ToResult();
        }
    }

    public async Task<Result> RegisterAccountAsync(RegisterAccountRequest registerRequest)
    {
        try
        {
            Name name = Name.Create(registerRequest.Name);
            Email email = Email.Create(registerRequest.Email);
            Password password = Password.Create(registerRequest.Password);

            bool accountExists = await _userRepository.GetByEmailAsync(email) != null;

            if (accountExists)
            {
                return Result.Failure(ResultCode.AccountAlreadyExistsError);
            }

            User user = User.Register(name, email, password);

            await _userRepository.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success(ResultCode.SignUpSuccessfully);
        }
        catch (Exception e)
        {
            return e.ToResult();
        }
    }

    public async Task<Result> VerifyAccountWithCodeAsync(SendAccountVerificationCodeRequest verificationCodeRequest)
    {
        try
        {
            Email email = Email.Create(verificationCodeRequest.Email);

            User? account = await _userRepository.GetByEmailAsync(email);

            if (account == null)
            {
                return Result.Failure(ResultCode.AccountNotFoundError);
            }

            account.VerifyVerificationCode(Code.FromCode(verificationCodeRequest.Code ?? ""));
            await _unitOfWork.SaveChangesAsync();

            string jwtToken = _jwt.GenerateJsonWebToken(account.Id, email.Value);

            AccountAccessTokenResponse accountAccessToken = new(jwtToken);

            return Result<AccountAccessTokenResponse>.Success(ResultCode.SignInSuccessfully, accountAccessToken);
        }
        catch (Exception e)
        {
            return e.ToResult();
        }
    }

    public async Task<Result> GetAccountDetailsAsync()
    {
        Guid userId = _userContext.GetUserId();

        User? account = await _userRepository.GetByIdAsync(userId);

        if (account == null)
        {
            return Result.Failure(ResultCode.AccountNotFoundError);
        }

        AccountDetailsResponse accountDetails = new(account.Id, account.Name.Value, account.Email.Value);

        return Result<AccountDetailsResponse>.Success(ResultCode.Success, accountDetails);
    }
}