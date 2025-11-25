namespace NoteX.Application.Users.DTOs.Responses;

public class AccountDetailsResponse(Guid userId, string name, string email)
{
    public Guid UserId { get; } = userId;
    public string Name { get; } = name;
    public string Email { get; } = email;
}