using NoteX.Domain.Users.ValueObjects;

namespace NoteX.Tests.Domain.Users.ValueObjects;

public class CodeTests
{
    [Fact]
    public void GivenValidCode_WhenCreatingCode_ThenCodeIsCreatedSuccessfully()
    {
        Code code = Code.Create();

        Assert.NotEmpty(code.Value);
        Assert.Equal(8, code.Value.Length);
        Assert.True(code.Value.All(char.IsDigit));
    }
}