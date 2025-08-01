using Inlog.Desafio.Backend.Application.Messaging;
using Inlog.Desafio.Backend.Domain.Vehicles;

namespace Inlog.Desafio.Backend.Application.Services.Vehicles.Update;

public sealed record UpdateVehicleCommand(
    Guid Id,
    string Identifier,
    string Chassis,
    string LicensePlate,
    string TrackerSerialNumber,
    int VehicleType,
    string Color,
    Coordinates Coordinates
) : ICommand;