using SharedKernel;

namespace Inlog.Desafio.Backend.Domain.Models;

public class Vehicle : Entity
{
    public Guid Id { get; init; }
    public required string Chassis { get; init; }
    public VehicleType VehicleType { get; init; }
    public required string Color { get; init; }
}