using Inlog.Desafio.Backend.Application.Messaging;
using Inlog.Desafio.Backend.Domain.Vehicles;

namespace Inlog.Desafio.Backend.Application.Services.Vehicles.Register;

public sealed record RegisterVehicleCommand(
    string Identifier,
    string Chassis,
    string LicensePlate,
    string TrackerSerialNumber,
    VehicleType VehicleType,
    string Color,
    Coordinates Coordinates
) : ICommand<Vehicle>;