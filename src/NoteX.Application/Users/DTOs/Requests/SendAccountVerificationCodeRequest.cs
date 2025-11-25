namespace NoteX.Application.Users.DTOs.Requests;

public class SendAccountVerificationCodeRequest(string? email, string? code)
{
    public string? Email { get; } = email;
    public string? Code { get; } = code;
}