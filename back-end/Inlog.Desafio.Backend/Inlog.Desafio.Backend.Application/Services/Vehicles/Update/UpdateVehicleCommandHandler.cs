using Inlog.Desafio.Backend.Application.Abstractions.Data;
using Inlog.Desafio.Backend.Application.Messaging;
using Inlog.Desafio.Backend.Domain.Vehicles;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Inlog.Desafio.Backend.Application.Services.Vehicles.Update;

public sealed class UpdateVehicleCommandHandler(IApplicationDbContext context)
    : ICommandHandler<UpdateVehicleCommand>
{
    public async Task<Result> Handle(UpdateVehicleCommand command, CancellationToken cancellationToken)
    {
        var vehicle = await context.Vehicles
            .FirstOrDefaultAsync(v => v.Id == command.Id, cancellationToken);

        if (vehicle is null)
            return Result.Failure(VehicleError.VehicleNotFound(command.Id));

        vehicle.Identifier = command.Identifier;
        vehicle.Chassis = command.Chassis;
        vehicle.LicensePlate = command.LicensePlate;
        vehicle.TrackerSerialNumber = command.TrackerSerialNumber;
        vehicle.VehicleType = (VehicleType)command.VehicleType;
        vehicle.Color = command.Color;
        vehicle.Coordinates = new Coordinates
        {
            Latitude = command.Coordinates.Latitude,
            Longitude = command.Coordinates.Longitude
        };

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}