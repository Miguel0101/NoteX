using System.Linq.Expressions;
using NoteX.Domain.Common.Interfaces;
using NoteX.Domain.Users.Entities;
using NoteX.Domain.Users.ValueObjects;

namespace NoteX.Domain.Users.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByEmailAsync(Email email);
    Task<IReadOnlyList<User>> FindAsync(Expression<Func<User, bool>> predicate);
    Task<IReadOnlyList<User>> GetAllAsync();
    Task<User?> GetByIdAsync(Guid id);
}