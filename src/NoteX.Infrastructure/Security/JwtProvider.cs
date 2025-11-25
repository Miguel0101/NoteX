using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NoteX.Application.Common.Interfaces;

namespace NoteX.Infrastructure.Security;

public class JwtProvider : IJwtProvider
{
    private readonly JwtSettings _jwtConfiguration;

    public JwtProvider(IOptions<JwtSettings> jwtConfiguration)
    {
        _jwtConfiguration = jwtConfiguration.Value;
    }

    public string GenerateJsonWebToken(Guid userId, string email)
    {
        JwtSecurityTokenHandler handler = new();

        SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_jwtConfiguration.PrivateKey));

        SigningCredentials credentials = new(key, SecurityAlgorithms.HmacSha512);

        SecurityTokenDescriptor tokenDescriptor = new()
        {
            SigningCredentials = credentials,
            Issuer = _jwtConfiguration.Issuer,
            Audience = _jwtConfiguration.Audience,
            Subject = GenerateClaims(userId, email),
            Expires = DateTime.UtcNow.AddMinutes(_jwtConfiguration.Lifetime)
        };

        SecurityToken token = handler.CreateToken(tokenDescriptor);
        return handler.WriteToken(token);
    }

    private static ClaimsIdentity GenerateClaims(Guid userId, string email)
    {
        ClaimsIdentity claimsIdentity = new();

        List<Claim> claims =
        [
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, email)
        ];

        claimsIdentity.AddClaims(claims);

        return claimsIdentity;
    }
}