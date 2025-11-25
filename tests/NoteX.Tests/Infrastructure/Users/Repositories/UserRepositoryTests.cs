using Microsoft.EntityFrameworkCore;
using NoteX.Domain.Users.Entities;
using NoteX.Domain.Users.ValueObjects;
using NoteX.Infrastructure.Data;
using NoteX.Infrastructure.Users.Repositories;

namespace NoteX.Tests.Infrastructure.Users.Repositories;

public class UserRepositoryTests
{
    private static ApplicationDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new ApplicationDbContext(options);
    }

    [Fact]
    public async Task GivenUser_WhenAddingUserOnDatabase_ThenUserIsAddedSuccessfully()
    {
        // Arrange
        using ApplicationDbContext context = GetInMemoryDbContext();
        UserRepository repo = new(context);
        User user = User.Register(Name.Create("Valid Name"), Email.Create("email@valid.com"), Password.Create("Valid Password"));

        // Act
        await repo.AddAsync(user);
        await context.SaveChangesAsync();

        // Assert
        var savedUser = await repo.GetByIdAsync(user.Id);
        Assert.NotNull(savedUser);
        Assert.Equal("Valid Name", savedUser.Name.Value);
        Assert.Equal("email@valid.com", savedUser.Email.Value);
        Assert.True(savedUser.Password.Verify("Valid Password"));
    }
}