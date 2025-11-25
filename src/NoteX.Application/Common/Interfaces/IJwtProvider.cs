namespace NoteX.Application.Common.Interfaces;

public interface IJwtProvider
{
    string GenerateJsonWebToken(Guid userId, string email);
}