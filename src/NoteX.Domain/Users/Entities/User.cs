using NoteX.Domain.Users.ValueObjects;

namespace NoteX.Domain.Users.Entities;

public class User
{
    public Ulid Id { get; private set; }
    public Name Name { get; private set; }
    public Email Email { get; private set; }
    public Password Password { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdateAt { get; private set; }

    public User(Name name, Email email, Password password)
    {
        Id = Ulid.NewUlid();
        Name = name;
        Email = email;
        Password = password;
        CreatedAt = DateTime.UtcNow;
        UpdateAt = null;
    }
}