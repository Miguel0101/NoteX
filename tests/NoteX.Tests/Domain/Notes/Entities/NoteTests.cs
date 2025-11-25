using NoteX.Domain.Notes.Entities;
using NoteX.Domain.Notes.ValueObjects;

namespace NoteX.Tests.Domain.Notes.Entities;

public class NoteTests
{
    [Fact]
    public void GivenValidValues_WhenCreatingNote_ThenNoteIsCreatedSuccessfully()
    {
        Guid userId = Guid.NewGuid();
        Title title = Title.Create("Title");
        Content content = Content.Create("Content");

        // Create a note
        Note note = Note.Create(userId, title, content);

        Assert.Equal(userId, note.UserId);
        Assert.Equal("Title", note.Title.Value);
        Assert.Equal("Content", note.Content.Value);
        Assert.True(note.CreatedAt <= DateTime.UtcNow);
        Assert.Null(note.UpdatedAt);
    }
}