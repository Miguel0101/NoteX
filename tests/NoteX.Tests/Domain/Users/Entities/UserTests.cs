using NoteX.Domain.Users.Entities;
using NoteX.Domain.Users.Exceptions;
using NoteX.Domain.Users.ValueObjects;

namespace NoteX.Tests.Domain.Users.Entities;

public class UserTests
{
    [Fact]
    public void GivenValidValues_WhenCreatingUser_ThenUserIsCreatedSuccessfully()
    {
        Name name = Name.Create("Valid name");
        Email email = Email.Create("email@valid.com");
        Password password = Password.Create("Valid password");

        User user = new(name, email, password);

        Assert.Equal("Valid name", user.Name.Value);
        Assert.Equal("email@valid.com", user.Email.Value);
        Assert.True(password.Verify("Valid password"));
    }

    [Fact]
    public void GivenNullName_WhenCreatingUser_ThenThrowsNameNullException()
    {
        Name name = null!;
        Email email = Email.Create("email@valid.com");
        Password password = Password.Create("Valid password");

        Assert.Throws<NameNullException>(() => { User user = new(name, email, password); });
    }

    [Fact]
    public void GivenNullEmail_WhenCreatingUser_ThenThrowsEmailNullException()
    {
        Name name = Name.Create("Valid name");
        Email email = null!;
        Password password = Password.Create("Valid password");

        Assert.Throws<EmailNullException>(() => { User user = new(name, email, password); });
    }

    [Fact]
    public void GivenNullPassword_WhenCreatingUser_ThenThrowsPasswordNullException()
    {
        Name name = Name.Create("Valid name");
        Email email = Email.Create("email@valid.com");
        Password password = null!;

        Assert.Throws<PasswordNullException>(() => { User user = new(name, email, password); });
    }
}