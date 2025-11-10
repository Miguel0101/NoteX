using NoteX.Domain.Users.Exceptions;
using NoteX.Domain.Users.ValueObjects;

namespace NoteX.Tests.Domain.Users.ValueObjects;

public class PasswordTests
{
    [Fact]
    public void GivenValidPassword_WhenCreatingPassword_ThenPasswordIsCreatedSuccessfully()
    {
        Password password = Password.Create("Valid password");

        Assert.True(password.Verify("Valid password"));
    }

    [Fact]
    public void GivenNullPassword_WhenCreatingPassword_ThenThrowsPasswordNullException()
    {
        Assert.Throws<PasswordNullException>(() => Password.Create(null!));
    }

    [Fact]
    public void GivenEmptyPassword_WhenCreatingPassword_ThenThrowsPasswordEmptyException()
    {
        Assert.Throws<PasswordEmptyException>(() => Password.Create(""));
    }

    [Fact]
    public void GivenShortPassword_WhenCreatingPassword_ThenThrowsPasswordOutRangeException()
    {
        string tooShort = new('A', 7);
        Assert.Throws<PasswordOutOfRangeException>(() => Password.Create(tooShort));
    }

    [Fact]
    public void GivenLongPassword_WhenCreatingPassword_ThenThrowsPasswordOutRangeException()
    {
        string tooLong = new('A', 129);
        Assert.Throws<PasswordOutOfRangeException>(() => Password.Create(tooLong));
    }
}