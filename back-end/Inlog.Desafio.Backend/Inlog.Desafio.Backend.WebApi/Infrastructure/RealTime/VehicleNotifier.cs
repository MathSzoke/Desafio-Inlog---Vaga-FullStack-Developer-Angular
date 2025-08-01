using Inlog.Desafio.Backend.Application.Abstractions;
using Inlog.Desafio.Backend.WebApi.Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Inlog.Desafio.Backend.WebApi.Infrastructure.RealTime;

public sealed class VehicleNotifier(IHubContext<VehicleHub> hubContext) : IRealTimeVehicleNotifier
{
    public async Task NotifyVehicleRegisteredAsync(object vehicleData, CancellationToken cancellationToken)
    {
        await hubContext.Clients.All.SendAsync("VehicleRegistered", vehicleData, cancellationToken);
    }
}