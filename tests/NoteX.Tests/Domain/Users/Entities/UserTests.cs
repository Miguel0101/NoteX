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

        User user = User.Register(name, email, password);

        Assert.Equal("Valid name", user.Name.Value);
        Assert.Equal("email@valid.com", user.Email.Value);
        Assert.True(password.Verify("Valid password"));
        Assert.Null(user.UpdatedAt);
    }

    [Fact]
    public void GivenValidValues_WhenGeneratingVerificationCode_ThenVerificationCodeIsCreatedSuccessfully()
    {
        Name name = Name.Create("Valid name");
        Email email = Email.Create("email@valid.com");
        Password password = Password.Create("Valid password");

        User user = User.Register(name, email, password);

        VerificationCode code = user.GenerateVerificationCode();

        Assert.Single(user.VerificationCodes);
        Assert.Equal(TimeSpan.FromMinutes(5), code.ExpiredAt - code.CreatedAt);
        Assert.Null(code.VerifiedAt);
    }

    [Fact]
    public void GivenValidValues_WhenVerifingVerificationCode_ThenVerificationCodeIsVerifiedSuccessfully()
    {
        Name name = Name.Create("Valid name");
        Email email = Email.Create("email@valid.com");
        Password password = Password.Create("Valid password");

        User user = User.Register(name, email, password);

        VerificationCode pendingCode = user.GenerateVerificationCode();
        VerificationCode verifiedCode = user.VerifyVerificationCode(pendingCode.Code);

        Assert.NotNull(verifiedCode.VerifiedAt);
        Assert.True(pendingCode.IsVerified());
    }

    [Fact]
    public void GivenValidValues_WhenGeneratingTwoVerificationCodes_ThenThrowsVerificationCodePendingException()
    {
        Name name = Name.Create("Valid name");
        Email email = Email.Create("email@valid.com");
        Password password = Password.Create("Valid password");

        User user = User.Register(name, email, password);

        user.GenerateVerificationCode();

        Assert.Throws<VerificationCodePendingException>(() => { user.GenerateVerificationCode(); });
    }

    [Fact]
    public void GivenInvalidCode_WhenVerifying_ThenThrowsVerificationCodeNotFoundException()
    {
        Name name = Name.Create("Valid name");
        Email email = Email.Create("email@valid.com");
        Password password = Password.Create("Valid password");

        User user = User.Register(name, email, password);

        Code code = Code.Create();

        Assert.Throws<VerificationCodeNotFoundException>(() => user.VerifyVerificationCode(code));
    }
}