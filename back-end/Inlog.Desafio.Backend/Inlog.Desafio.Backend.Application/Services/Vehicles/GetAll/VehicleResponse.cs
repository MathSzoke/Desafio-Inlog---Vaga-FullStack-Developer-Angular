using Inlog.Desafio.Backend.Domain.Vehicles;

namespace Inlog.Desafio.Backend.Application.Services.Vehicles.GetAll;

public sealed record CoordinatesResponse(double Latitude, double Longitude);
public sealed record VehiclesResponse(
    Guid Id,
    string Chassis,
    string Identifier,
    string LicensePlate,
    string TrackerSerialNumber,
    string Color,
    VehicleType VehicleType,
    CoordinatesResponse Coordinates);