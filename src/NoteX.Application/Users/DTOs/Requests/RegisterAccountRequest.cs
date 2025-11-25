namespace NoteX.Application.Users.DTOs.Requests;

public class RegisterAccountRequest(string? name, string? email, string? password)
{
    public string? Name { get; set; } = name;
    public string? Email { get; private set; } = email;
    public string? Password { get; private set; } = password;
}