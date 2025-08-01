using Inlog.Desafio.Backend.Application.Abstractions.Data;
using Inlog.Desafio.Backend.Application.Messaging;
using Inlog.Desafio.Backend.Domain.Vehicles;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Inlog.Desafio.Backend.Application.Services.Vehicles.Delete;

public sealed class DeleteVehicleCommandHandler(IApplicationDbContext context) : ICommandHandler<DeleteVehicleCommand>
{
    public async Task<Result> Handle(DeleteVehicleCommand command,
        CancellationToken cancellationToken)
    {
        var vehicle = await context.Vehicles
            .FirstOrDefaultAsync(v => v.Id == command.Id, cancellationToken: cancellationToken);
        
        if (vehicle is null) return Result.Failure(VehicleError.VehicleNotFound(command.Id));

        vehicle.IsDeleted = true;
        context.Vehicles.Update(vehicle);

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success(vehicle.Id);
    }
}