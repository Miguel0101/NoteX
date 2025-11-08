namespace NoteX.Domain.Notes.Entities;

public class Note
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public Title Title { get; private set; } = null!;
    public Description Description { get; private set; } = null!;
    public DateTime CreatedAt { get; private set; }

    public Note(Guid userId, Title title, Description description)
    {
        UserId = userId;
        Title = title;
        Description = description;
        CreatedAt = DateTime.UtcNow;
    }
}