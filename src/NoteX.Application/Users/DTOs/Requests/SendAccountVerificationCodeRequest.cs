namespace NoteX.Application.Users.DTOs.Requests;

public record SendAccountVerificationCodeRequest(string? Email, string? Code);