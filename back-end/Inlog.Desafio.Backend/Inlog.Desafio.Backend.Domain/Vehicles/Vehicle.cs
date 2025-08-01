using SharedKernel;

namespace Inlog.Desafio.Backend.Domain.Vehicles;

public class Vehicle : Entity
{
    public Guid Id { get; set; }
    public required string Chassis { get; set; }
    public required string Identifier { get; set; } // Nome/Identificador
    public required string LicensePlate { get; set; }
    public required string TrackerSerialNumber { get; set; }
    public required string Color { get; set; }
    public VehicleType VehicleType { get; set; }
    public Coordinates Coordinates { get; set; } = null!;
}