using NoteX.Domain.Notes.Exceptions;
using NoteX.Domain.Notes.ValueObjects;

namespace NoteX.Tests.Domain.Notes.ValueObjects;

public class TitleTests
{
    [Fact]
    public void GivenValidTitle_WhenCreatingTitle_ThenTitleIsCreatedSuccessfully()
    {
        Title title = Title.Create("Valid title");

        Assert.Equal("Valid title", title.Value);
    }

    [Fact]
    public void GivenNullTitle_WhenCreatingTitle_ThenThrowsTitleNullException()
    {
        Assert.Throws<TitleNullException>(() => Title.Create(null!));
    }

    [Fact]
    public void GivenEmptyTitle_WhenCreatingTitle_ThenThrowsTitleEmptyException()
    {
        Assert.Throws<TitleEmptyException>(() => Title.Create(""));
    }

    [Fact]
    public void GivenShortTitle_WhenCreatingTitle_ThenThrowsTitleOutRangeException()
    {
        string tooShort = new('A', 4);
        Assert.Throws<TitleOutOfRangeException>(() => Title.Create(tooShort));
    }

    [Fact]
    public void GivenLongTitle_WhenCreatingTitle_ThenThrowsTitleOutRangeException()
    {
        string tooLong = new('A', 101);
        Assert.Throws<TitleOutOfRangeException>(() => Title.Create(tooLong));
    }
}