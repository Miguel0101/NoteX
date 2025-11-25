namespace NoteX.Application.Users.DTOs.Responses;

public class AccountAccessTokenResponse(string token)
{
    public string Token { get; } = token;
}