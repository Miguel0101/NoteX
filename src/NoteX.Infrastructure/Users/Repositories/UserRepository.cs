using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using NoteX.Domain.Users.Entities;
using NoteX.Domain.Users.Interfaces;
using NoteX.Domain.Users.ValueObjects;
using NoteX.Infrastructure.Common.Repositories;
using NoteX.Infrastructure.Data;

namespace NoteX.Infrastructure.Users.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context) {} 

    public async Task<User?> GetByEmailAsync(Email email)
    {
        return await _context.Users
            .Include(u => u.VerificationCodes)
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<IReadOnlyList<User>> FindAsync(Expression<Func<User, bool>> predicate)
    {
        return await _context.Users
            .AsNoTracking()
            .Where(predicate)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<User>> GetAllAsync()
    {
        return await _context.Users
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _context.Users
            .Include(u => u.VerificationCodes)
            .FirstOrDefaultAsync(u => u.Id == id);
    }
}