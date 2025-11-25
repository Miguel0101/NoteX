using NoteX.Domain.Notes.Entities;
using NoteX.Domain.Notes.Exceptions;
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

    [Fact]
    public void GivenNullTitle_WhenCreatingNote_ThenThrowsTitleNullException()
    {
        Guid userId = Guid.NewGuid();
        Title title = null!;
        Content content = Content.Create("Content");

        // Create a note
        Assert.Throws<TitleNullException>(() => { Note note = Note.Create(userId, title, content); });
    }

    [Fact]
    public void GivenNullContent_WhenCreatingNote_ThenThrowsContentNullException()
    {
        Guid userId = Guid.NewGuid();
        Title title = Title.Create("Title");
        Content content = null!;

        // Create a note
        Assert.Throws<ContentNullException>(() => { Note note = Note.Create(userId, title, content); });
    }
}