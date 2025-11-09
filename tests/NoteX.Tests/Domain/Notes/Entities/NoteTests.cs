using NoteX.Domain.Notes.Entities;
using NoteX.Domain.Notes.Exceptions;
using NoteX.Domain.Notes.ValueObjects;

namespace NoteX.Tests.Domain.Notes.Entities;

public class NoteTests
{
    [Fact]
    public void GivenValidValues_WhenCreatingNote_ThenNoteIsCreatedSuccessfully()
    {
        Ulid userId = Ulid.NewUlid();
        Title title = Title.Create("Title");
        Content content = Content.Create("Content");

        // Create a note
        Note note = new(userId, title, content);

        Assert.Equal(userId, note.UserId);
        Assert.Equal("Title", note.Title.Value);
        Assert.Equal("Content", note.Content.Value);
        Assert.True(note.CreatedAt <= DateTime.UtcNow);
        Assert.Null(note.UpdatedAt);
    }

    [Fact]
    public void GivenNullTitle_WhenCreatingNote_ThenThrowsTitleNullException()
    {
        Ulid userId = Ulid.NewUlid();
        Title title = null!;
        Content content = Content.Create("Content");

        // Create a note
        Assert.Throws<TitleNullException>(() => { Note note = new(userId, title, content); });
    }

    [Fact]
    public void GivenNullContent_WhenCreatingNote_ThenThrowsContentNullException()
    {
        Ulid userId = Ulid.NewUlid();
        Title title = Title.Create("Title");
        Content content = null!;

        // Create a note
        Assert.Throws<ContentNullException>(() => { Note note = new(userId, title, content); });
    }
}