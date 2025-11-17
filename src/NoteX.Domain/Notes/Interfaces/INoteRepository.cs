using NoteX.Domain.Common.Interfaces;
using NoteX.Domain.Notes.Entities;
using NoteX.Domain.Notes.ValueObjects;

namespace NoteX.Domain.Notes.Interfaces;

public interface INoteRepository : IRepository<Note>
{
    Task<Note> GetByTitleAsync(Title title);
}