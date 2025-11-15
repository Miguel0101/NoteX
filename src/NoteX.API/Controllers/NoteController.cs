using Microsoft.AspNetCore.Mvc;

namespace NoteX.API.Controllers;

[ApiController]
[Route("/api/notes")]
public class NoteController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok();
    }

    [HttpGet("{id}")]
    public IActionResult Get(Ulid id)
    {
        return Ok();
    }

    [HttpPost]
    public IActionResult Add()
    {
        return Created();
    }

    [HttpPut("{id}")]
    public IActionResult Update(Ulid id)
    {
        return Ok();
    }

    [HttpPatch("{id}/title")]
    public IActionResult UpdateTitle(Ulid id, [FromBody] string title)
    {
        return Ok();
    }

    [HttpPatch("{id}/content")]
    public IActionResult UpdateContent(Ulid id, [FromBody] string content)
    {
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Ulid id)
    {
        return Ok();
    }
}