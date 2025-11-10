using NoteX.Domain.Users.Exceptions;
using NoteX.Domain.Users.ValueObjects;

namespace NoteX.Tests.Domain.Users.ValueObjects;

public class EmailTests
{
    [Fact]
    public void GivenValidEmail_WhenCreatingEmail_ThenEmailIsCreatedSuccessfully()
    {
        Email email = Email.Create("email@valid.com");

        Assert.Equal("email@valid.com", email.Value);
    }

    [Fact]
    public void GivenNullEmail_WhenCreatingEmail_ThenThrowsEmailNullException()
    {
        Assert.Throws<EmailNullException>(() => Email.Create(null!));
    }

    [Fact]
    public void GivenEmptyEmail_WhenCreatingEmail_ThenThrowsEmailEmptyException()
    {
        Assert.Throws<EmailEmptyException>(() => Email.Create(""));
    }

    [Theory]
    [InlineData("invalid")]
    [InlineData("invalid.com")]
    [InlineData("invalid@")]
    [InlineData("@invalid.com")]
    [InlineData("@")]
    public void GivenInvalidEmail_WhenCreatingEmail_ThenThrowsEmailFormatException(string input)
    {
        Assert.Throws<EmailFormatException>(() => Email.Create(input));
    }
}