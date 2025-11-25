using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoteX.API.Mappers;
using NoteX.Application.Users.DTOs.Requests;
using NoteX.Application.Users.Services;

namespace NoteX.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> RequestUserVerificationCode(LoginAccontRequest request)
    {
        var result = await _authService.LoginWithCredentialsAsync(request);

        return result.ToActionResult();
    }

    [HttpPost("login/verify")]
    public async Task<IActionResult> VerifyUserVerificationCode(SendAccountVerificationCodeRequest request)
    {
        var result = await _authService.VerifyAccountWithCodeAsync(request);

        return result.ToActionResult();
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterAccountRequest request)
    {
        var result = await _authService.RegisterAccountAsync(request);

        return result.ToActionResult();
    }

    [HttpGet("details")]
    [Authorize]
    public async Task<IActionResult> Details()
    {
        var result = await _authService.GetAccountDetailsAsync();

        return result.ToActionResult();
    }
}