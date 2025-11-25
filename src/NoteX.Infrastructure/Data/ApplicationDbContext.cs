using Microsoft.EntityFrameworkCore;
using NoteX.Domain.Notes.Entities;
using NoteX.Domain.Users.Entities;
using NoteX.Infrastructure.Notes.Configurations;
using NoteX.Infrastructure.Users.Configurations;

namespace NoteX.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Note> Notes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new VerificationCodeConfiguration());
        modelBuilder.ApplyConfiguration(new NoteConfiguration());
    }
}