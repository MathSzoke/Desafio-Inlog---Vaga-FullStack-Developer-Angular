using Inlog.Desafio.Backend.Application.Abstractions;
using Inlog.Desafio.Backend.Domain.Vehicles;

namespace Inlog.Desafio.Backend.Application.Services.Vehicles.Register;

public class VehicleRegisteredDomainEventHandler(IRealTimeVehicleNotifier notifier)
{
    public async Task Handle(VehicleRegisteredDomainEvent notification, CancellationToken cancellationToken)
    {
        var vehicle = notification.Vehicle;

        var vehicleData = new
        {
            vehicle.Id,
            vehicle.Identifier,
            vehicle.LicensePlate,
            vehicle.TrackerSerialNumber,
            vehicle.Color,
            vehicle.VehicleType,
            Coordinates = new
            {
                vehicle.Coordinates.Latitude,
                vehicle.Coordinates.Longitude
            }
        };

        await notifier.NotifyVehicleRegisteredAsync(vehicleData, cancellationToken);
    }
}