namespace NoteX.Application.Users.DTOs.Responses;

public record AccountDetailsResponse(Guid UserId, string Name, string Email);