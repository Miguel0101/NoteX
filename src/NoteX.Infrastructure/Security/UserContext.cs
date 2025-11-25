using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using NoteX.Application.Common.Interfaces;

namespace NoteX.Infrastructure.Security;

public class UserContext : IUserContext
{
    private readonly IHttpContextAccessor _httpContext;

    public UserContext(IHttpContextAccessor httpContext)
    {
        _httpContext = httpContext;
    }

    public Guid GetUserId()
    {
        _ = Guid.TryParse(_httpContext.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId);

        return userId;
    }
}