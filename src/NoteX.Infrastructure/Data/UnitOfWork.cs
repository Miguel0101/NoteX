using NoteX.Application.Common.Dispatching;
using NoteX.Application.Common.Interfaces;
using NoteX.Domain.Common.Entities;

namespace NoteX.Infrastructure.Data;

public class UnitOfWork : IUnitOfWork
{
	private readonly ApplicationDbContext _context;
	private readonly IEventDispatcher _dispatcher;

	public UnitOfWork(ApplicationDbContext context, IEventDispatcher dispatcher)
	{
		_context = context;
		_dispatcher = dispatcher;
	}

	public async Task BeginTransactionAsync()
	{
		await _context.Database.BeginTransactionAsync();
	}

	public async Task CommitAsync()
	{
		await _context.Database.CommitTransactionAsync();
	}

	public async Task RollbackAsync()
	{
		await _context.Database.RollbackTransactionAsync();
	}

	public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
	{
		var events = _context.ChangeTracker
			.Entries<AggregateRoot>()
			.SelectMany(e => e.Entity.DomainEvents)
			.ToList();

		foreach (var e in _context.ChangeTracker.Entries<AggregateRoot>())
			e.Entity.ClearDomainEvents();

		var result = await _context.SaveChangesAsync(cancellationToken);

		foreach (var domainEvent in events)
			await _dispatcher.DispatchAsync(domainEvent, cancellationToken);

		return result;

	}
}
