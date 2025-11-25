using NoteX.Application.Common.Results;
using NoteX.Application.Notes.DTOs.Requests;

namespace NoteX.Application.Notes.Services;

public interface INoteService
{
    Task<Result> GetAllAsync();
    Task<Result> GetByIdAsync(Guid id);
    Task<Result> AddAsync(CreateNoteRequest createNoteRequest);
    Task<Result> UpdateAsync(Guid id, UpdateNoteRequest updateNoteRequest);
    Task<Result> UpdateTitleAsync(Guid id, UpdateTitleRequest updateTitleRequest);
    Task<Result> UpdateContentAsync(Guid id, UpdateContentRequest updateContentRequest);
    Task<Result> DeleteAsync(Guid id);
}