using NoteX.Domain.Users.Exceptions;
using NoteX.Domain.Users.ValueObjects;

namespace NoteX.Tests.Domain.Users.ValueObjects;

public class NameTests
{
    [Fact]
    public void GivenValidName_WhenCreatingName_ThenNameIsCreatedSuccessfully()
    {
        Name name = Name.Create("Valid name");

        Assert.Equal("Valid name", name.Value);
    }

    [Fact]
    public void GivenNullName_WhenCreatingName_ThenThrowsNameNullException()
    {
        Assert.Throws<NameNullException>(() => Name.Create(null!));
    }

    [Fact]
    public void GivenEmptyName_WhenCreatingName_ThenThrowsNameEmptyException()
    {
        Assert.Throws<NameEmptyException>(() => Name.Create(""));
    }

    [Fact]
    public void GivenShortName_WhenCreatingName_ThenThrowsNameOutRangeException()
    {
        string tooShort = new('A', 2);
        Assert.Throws<NameOutOfRangeException>(() => Name.Create(tooShort));
    }

    [Fact]
    public void GivenLongName_WhenCreatingName_ThenThrowsNameOutRangeException()
    {
        string tooLong = new('A', 51);
        Assert.Throws<NameOutOfRangeException>(() => Name.Create(tooLong));
    }
}