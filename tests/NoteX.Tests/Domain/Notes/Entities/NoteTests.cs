using NoteX.Domain.Notes.Entities;
using Xunit;

namespace NoteX.Tests.Domain.Notes.Entities;

public class NoteTests
{
    [Fact]
    public void AddNoteOk()
    {
        Guid userId = Guid.NewGuid();
        Title title = Title.Create("Title");
        Description description = Description.Create("Description");

        // Create a note
        Note note = new(userId, title, description);

        Assert.Equal(note.UserId, userId);
        Assert.Equal(note.Title.Value, "Title");
        Assert.Equal(note.Description.Value, "Description");
    }
}