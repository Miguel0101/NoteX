using NoteX.Domain.Notes.ValueObjects;

namespace NoteX.Domain.Notes.Entities;

public class Note
{
    public Ulid Id { get; private set; }
    public Ulid UserId { get; private set; }
    public Title Title { get; private set; } = null!;
    public Content Content { get; private set; } = null!;
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    public Note(Ulid userId, Title title, Content content)
    {
        Title.Validate(title);
        Content.Validate(content);

        Id = Ulid.NewUlid();
        UserId = userId;
        Title = title;
        Content = content;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = null;
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