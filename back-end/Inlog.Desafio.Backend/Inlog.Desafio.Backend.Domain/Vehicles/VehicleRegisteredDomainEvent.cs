using SharedKernel;

namespace Inlog.Desafio.Backend.Domain.Vehicles;

public sealed record VehicleRegisteredDomainEvent(Vehicle Vehicle) : IDomainEvent;