using NoteX.Domain.Notes.ValueObjects;
using NoteX.Domain.Users.Entities;

namespace NoteX.Domain.Notes.Entities;

public class Note
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public Title Title { get; private set; }
    public Content Content { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    public User User { get; } = null!;

    private Note(Guid userId, Title title, Content content)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Title = title;
        Content = content;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = null;
    }

    public static Note Create(Guid userId, Title title, Content content)
    {
        Note note = new(userId, title, content);

        return note;
    }

    public Note UpdateTitle(Title title)
    {
        if (title == Title)
            return this;

        Title = title;
        UpdatedAt = DateTime.UtcNow;

        return this;
    }

    public Note UpdateContent(Content content)
    {
        if (content == Content)
            return this;

        Content = content;
        UpdatedAt = DateTime.UtcNow;

        return this;
    }
}