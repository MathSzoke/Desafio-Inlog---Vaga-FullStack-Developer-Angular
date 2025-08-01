using Inlog.Desafio.Backend.Application.Abstractions.Data;
using Inlog.Desafio.Backend.Domain.Models;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using SharedKernel.DomainEvents;

namespace Inlog.Desafio.Backend.Infra.Database.Database;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly ICurrentUserContext? _currentUserContext;
    private readonly DomainEventsDispatcher? _domainEventsDispatcher;

    public ApplicationDbContext(DbContextOptions options,
        ICurrentUserContext? currentUserContext,
        DomainEventsDispatcher? domainEventsDispatcher) : base(options)
    {
        _currentUserContext = currentUserContext;
        _domainEventsDispatcher = domainEventsDispatcher;
    }

    public DbSet<Veiculo> Veiculos { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema(Schemas.Default);
    }
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var currentUserId = _currentUserContext?.UserId;

        foreach (var entry in ChangeTracker.Entries<Miscellaneous>())
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = currentUserId;
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    entry.Entity.UpdatedBy = currentUserId;
                    entry.Entity.UpdatedAt = entry.Entity.CreatedAt;
                    break;

                case EntityState.Modified:
                    entry.Entity.UpdatedBy = currentUserId;
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    break;

                case EntityState.Deleted:
                    entry.State = EntityState.Modified;
                    entry.Entity.IsDeleted = true;
                    entry.Entity.DeletedBy = currentUserId;
                    entry.Entity.DeletedAt = DateTime.UtcNow;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

        var domainEvents = ChangeTracker.Entries<Entity>()
            .Select(e => e.Entity)
            .Where(e => e.DomainEvents.Count != 0)
            .SelectMany(e =>
            {
                var events = e.DomainEvents.ToList();
                e.ClearDomainEvents();
                return events;
            })
            .ToList();

        var result = await base.SaveChangesAsync(cancellationToken);

        if (_domainEventsDispatcher is not null && domainEvents.Count != 0)
            await _domainEventsDispatcher.DispatchAsync(domainEvents, cancellationToken);

        return result;
    }
}