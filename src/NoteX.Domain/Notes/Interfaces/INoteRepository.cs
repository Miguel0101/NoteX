using System.Linq.Expressions;
using NoteX.Domain.Common.Interfaces;
using NoteX.Domain.Notes.Entities;
using NoteX.Domain.Notes.ValueObjects;

namespace NoteX.Domain.Notes.Interfaces;

public interface INoteRepository : IRepository<Note>
{
    Task<Note?> GetByTitleAsync(Guid userId, Title title);
    Task<IReadOnlyList<Note>> FindAsync(Guid userId, Expression<Func<Note, bool>> predicate);
    Task<IReadOnlyList<Note>> GetAllAsync(Guid userId);
    Task<Note?> GetByIdAsync(Guid userId, Guid id);
}