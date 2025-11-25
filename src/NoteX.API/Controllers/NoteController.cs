using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoteX.API.Mappers;
using NoteX.Application.Notes.DTOs.Requests;
using NoteX.Application.Notes.Services;

namespace NoteX.API.Controllers;

[ApiController]
[Route("/api/notes")]
[Authorize]
public class NoteController : ControllerBase
{
    private readonly INoteService _noteService;

    public NoteController(INoteService noteService)
    {
        _noteService = noteService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _noteService.GetAllAsync();

        return result.ToActionResult();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(Guid id)
    {
        var result = await _noteService.GetByIdAsync(id);

        return result.ToActionResult();
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync(CreateNoteRequest request)
    {
        var result = await _noteService.AddAsync(request);

        return result.ToActionResult();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, UpdateNoteRequest request)
    {
        var result = await _noteService.UpdateAsync(id, request);

        return result.ToActionResult();
    }

    [HttpPatch("{id}/title")]
    public async Task<IActionResult> UpdateTitleAsync(Guid id, UpdateTitleRequest request)
    {
        var result = await _noteService.UpdateTitleAsync(id, request);

        return result.ToActionResult();
    }

    [HttpPatch("{id}/content")]
    public async Task<IActionResult> UpdateContentAsync(Guid id, UpdateContentRequest request)
    {
        var result = await _noteService.UpdateContentAsync(id, request);

        return result.ToActionResult();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var result = await _noteService.DeleteAsync(id);

        return result.ToActionResult();
    }
}