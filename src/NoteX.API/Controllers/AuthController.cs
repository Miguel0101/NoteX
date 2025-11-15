using Microsoft.AspNetCore.Mvc;

namespace NoteX.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    [HttpPost("login")]
    public IActionResult Login()
    {
        return Ok();
    }

    [HttpPost("register")]
    public IActionResult Register()
    {
        return Created();
    }

    [HttpPost("verify")]
    public IActionResult Verify()
    {
        return Ok();
    }

    [HttpGet("details")]
    public IActionResult Details()
    {
        return Ok();
    }
}