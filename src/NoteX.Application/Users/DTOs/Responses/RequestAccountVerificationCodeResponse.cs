namespace NoteX.Application.Users.DTOs.Responses;

public class RequestAccountVerificationCodeResponse(string email)
{
    public string Email { get; } = email;
}