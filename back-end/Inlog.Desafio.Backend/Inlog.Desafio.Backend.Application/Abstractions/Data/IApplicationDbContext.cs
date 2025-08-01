using Inlog.Desafio.Backend.Domain.Vehicles;
using Microsoft.EntityFrameworkCore;

namespace Inlog.Desafio.Backend.Application.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<Vehicle> Vehicles { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}