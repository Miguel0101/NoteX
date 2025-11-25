using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using NoteX.Domain.Notes.Entities;
using NoteX.Domain.Notes.Interfaces;
using NoteX.Domain.Notes.ValueObjects;
using NoteX.Infrastructure.Common.Repositories;
using NoteX.Infrastructure.Data;

namespace NoteX.Infrastructure.Notes.Repositories;

public class NoteRepository : Repository<Note>, INoteRepository
{
    public NoteRepository(ApplicationDbContext context) : base(context) {}

    public async Task<Note?> GetByTitleAsync(Guid userId, Title title)
    {
        return await _context.Notes
            .FirstOrDefaultAsync(n => n.UserId == userId && n.Title == title);
    }

    public async Task<IReadOnlyList<Note>> FindAsync(Guid userId, Expression<Func<Note, bool>> predicate)
    {
        return await _context.Notes
            .AsNoTracking()
            .Where(n => n.UserId == userId)
            .Where(predicate)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<Note>> GetAllAsync(Guid userId)
    {
        return await _context.Notes
            .AsNoTracking()
            .Where(n => n.UserId == userId)
            .ToListAsync();
    }

    public async Task<Note?> GetByIdAsync(Guid userId, Guid id)
    {
        return await _context.Notes
            .FirstOrDefaultAsync(n => n.UserId == userId && n.Id == id);
    }
}