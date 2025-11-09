using NoteX.Domain.Notes.Exceptions;
using NoteX.Domain.Notes.ValueObjects;

namespace NoteX.Tests.Domain.Notes.ValueObjects;

public class ContentTests
{
    [Fact]
    public void GivenValidContent_WhenCreatingContent_ThenContentIsCreatedSuccessfully()
    {
        Content content = Content.Create("Valid content");

        Assert.Equal("Valid content", content.Value);
    }

    [Fact]
    public void GivenNullContent_WhenCreatingContent_ThenThrowsContentNullException()
    {
        Assert.Throws<ContentNullException>(() => Content.Create(null!));
    }

    [Fact]
    public void GivenLongContent_WhenCreatingContent_ThenThrowsContentOutOfRangeException()
    {
        string tooLong = new('A', 3001);
        Assert.Throws<ContentOutOfRangeException>(() => Content.Create(tooLong));
    }
}