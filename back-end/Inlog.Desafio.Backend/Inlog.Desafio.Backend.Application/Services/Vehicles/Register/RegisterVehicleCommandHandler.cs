using Inlog.Desafio.Backend.Application.Abstractions.Data;
using Inlog.Desafio.Backend.Application.Messaging;
using Inlog.Desafio.Backend.Domain.Vehicles;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Inlog.Desafio.Backend.Application.Services.Vehicles.Register;

public sealed class RegisterVehicleCommandHandler(
    IApplicationDbContext context) : ICommandHandler<RegisterVehicleCommand, Vehicle>
{
    public async Task<Result<Vehicle>> Handle(RegisterVehicleCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = await ValidateCommandAsync(command, cancellationToken);
        if (validationResult.IsFailure) return Result.Failure<Vehicle>(validationResult.Error);
        
        var vehicle = new Vehicle
        {
            Id = Guid.NewGuid(),
            Identifier = command.Identifier,
            Chassis = command.Chassis,
            LicensePlate = command.LicensePlate,
            TrackerSerialNumber = command.TrackerSerialNumber,
            VehicleType = command.VehicleType,
            Color = command.Color,
            Coordinates = new Coordinates
            {
                Latitude = command.Coordinates.Latitude,
                Longitude = command.Coordinates.Longitude
            }
        };

        await context.Vehicles.AddAsync(vehicle, cancellationToken);

        vehicle.Raise(new VehicleRegisteredDomainEvent(vehicle));

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success(vehicle);
    }
    
    private async Task<Result> ValidateCommandAsync(RegisterVehicleCommand command, CancellationToken cancellationToken)
    {
        if (await context.Vehicles.AnyAsync(v => v.Chassis == command.Chassis, cancellationToken))
            return Result.Failure(VehicleError.VehicleAlreadyLinked(command.Chassis));

        if (await context.Vehicles.AnyAsync(v => v.LicensePlate == command.LicensePlate, cancellationToken))
            return Result.Failure(VehicleError.LicensePlateAlreadyInUse(command.LicensePlate));

        if (await context.Vehicles.AnyAsync(v => v.TrackerSerialNumber == command.TrackerSerialNumber, cancellationToken))
            return Result.Failure(VehicleError.TrackerSerialAlreadyInUse(command.TrackerSerialNumber));

        return Result.Success();
    }

}