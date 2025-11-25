namespace NoteX.Application.Users.DTOs.Requests;

public class LoginAccontRequest(string? email, string? password)
{
    public string? Email { get; private set; } = email;
    public string? Password { get; private set; } = password;
}