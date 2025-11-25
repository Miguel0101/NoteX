using Microsoft.EntityFrameworkCore;
using NoteX.Domain.Notes.Entities;
using NoteX.Domain.Notes.ValueObjects;
using NoteX.Domain.Users.Entities;
using NoteX.Domain.Users.ValueObjects;
using NoteX.Infrastructure.Data;
using NoteX.Infrastructure.Notes.Repositories;
using NoteX.Infrastructure.Users.Repositories;

namespace NoteX.Tests.Infrastructure.Notes.Repositories;

public class NoteRepositoryTests
{
    private static ApplicationDbContext GetSqliteInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlite("Filename=:memory:")
            .Options;

        var context = new ApplicationDbContext(options);
        context.Database.OpenConnection();
        context.Database.EnsureCreated();
        return context;
    }

    [Fact]
    public async Task GivenUserAndNote_WhenAddingNoteOnDatabase_ThenNoteIsAddedSuccessfully()
    {
        using var context = GetSqliteInMemoryDbContext();
        var userRepo = new UserRepository(context);
        var noteRepo = new NoteRepository(context);

        var user = User.Register(Name.Create("Valid Name"), Email.Create("email@valid.com"), Password.Create("Valid Password"));
        var note = Note.Create(user.Id, Title.Create("Valid Title"), Content.Create("Valid Content"));

        await userRepo.AddAsync(user);
        await noteRepo.AddAsync(note);
        await context.SaveChangesAsync();

        var savedNote = await noteRepo.GetByIdAsync(user.Id, note.Id);

        Assert.NotNull(savedNote);
        Assert.Equal(user.Id, savedNote.UserId);
        Assert.Equal("Valid Title", savedNote.Title.Value);
        Assert.Equal("Valid Content", savedNote.Content.Value);
    }

    [Fact]
    public async Task GivenMultipleNotes_WhenFindByPredicateAndUserId_ThenOnlyUserNotesAreReturned()
    {
        using var context = GetSqliteInMemoryDbContext();
        var userRepo = new UserRepository(context);
        var noteRepo = new NoteRepository(context);

        var user1 = User.Register(Name.Create("Valid Name1"), Email.Create("email1@valid.com"), Password.Create("Valid Password1"));
        var user2 = User.Register(Name.Create("Valid Name2"), Email.Create("email2@valid.com"), Password.Create("Valid Password2"));

        var note1 = Note.Create(user1.Id, Title.Create("Title1"), Content.Create("Content1"));
        var note2 = Note.Create(user1.Id, Title.Create("Title2"), Content.Create("Content2"));
        var note3 = Note.Create(user2.Id, Title.Create("Title3"), Content.Create("Content3"));

        await userRepo.AddAsync(user1);
        await userRepo.AddAsync(user2);
        await noteRepo.AddAsync(note1);
        await noteRepo.AddAsync(note2);
        await noteRepo.AddAsync(note3);

        await context.SaveChangesAsync();

        var results = (await noteRepo.GetAllAsync(user1.Id))
            .Where(n => n.Title.Value.Contains("Title"));

        Assert.Equal(2, results.Count());
        Assert.All(results, n => Assert.Equal(user1.Id, n.UserId));
    }
}
