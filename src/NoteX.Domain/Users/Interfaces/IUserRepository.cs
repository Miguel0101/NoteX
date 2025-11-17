using NoteX.Domain.Common.Interfaces;
using NoteX.Domain.Users.Entities;
using NoteX.Domain.Users.ValueObjects;

namespace NoteX.Domain.Users.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User> GetByEmailAsync(Email email);
}