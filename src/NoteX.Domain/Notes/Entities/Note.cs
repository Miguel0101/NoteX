using NoteX.Domain.Notes.ValueObjects;
using NoteX.Domain.Users.Entities;

namespace NoteX.Domain.Notes.Entities;

/// <summary>
/// Note - Domain Model
/// <para>Represents a note with title and content.</para>
/// </summary>
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

    /// <summary>
    /// Creates a new note with the user ID, title and content.
    /// </summary>
    /// <param name="userId">The owner of this note</param>
    /// <param name="title">The title</param>
    /// <param name="content">The content</param>
    /// <returns>The created note.</returns>
    public static Note Create(Guid userId, Title title, Content content)
    {
        Note note = new(userId, title, content);

        return note;
    }

    /// <summary>
    /// Updates the title of a existing note.
    /// </summary>
    /// <param name="title">The updated title</param>
    /// <returns>The updated note.</returns>
    public Note UpdateTitle(Title title)
    {
        if (title == Title)
            return this;

        Title = title;
        UpdatedAt = DateTime.UtcNow;

        return this;
    }

    /// <summary>
    /// Updates the content of a existing note.
    /// </summary>
    /// <param name="content">The updated content</param>
    /// <returns>The updated note.</returns>
    public Note UpdateContent(Content content)
    {
        if (content == Content)
            return this;

        Content = content;
        UpdatedAt = DateTime.UtcNow;

        return this;
    }
}