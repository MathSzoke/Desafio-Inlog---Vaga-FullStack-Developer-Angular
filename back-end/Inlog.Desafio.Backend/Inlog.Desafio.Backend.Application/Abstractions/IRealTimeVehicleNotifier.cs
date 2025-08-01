namespace Inlog.Desafio.Backend.Application.Abstractions;

public interface IRealTimeVehicleNotifier
{
    Task NotifyVehicleRegisteredAsync(object vehicleData, CancellationToken cancellationToken);
}