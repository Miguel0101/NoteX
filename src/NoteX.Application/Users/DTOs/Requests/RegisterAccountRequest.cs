namespace NoteX.Application.Users.DTOs.Requests;

public record RegisterAccountRequest(string? Name, string? Email, string? Password);