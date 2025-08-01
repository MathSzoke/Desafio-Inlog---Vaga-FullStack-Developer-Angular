using Inlog.Desafio.Backend.Application.Abstractions.Data;
using Inlog.Desafio.Backend.Application.Messaging;
using Inlog.Desafio.Backend.Domain.Vehicles;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Inlog.Desafio.Backend.Application.Services.Vehicles.GetAll;

public sealed class GetVehiclesQueryHandler(
    IApplicationDbContext context)
    : IQueryHandler<GetVehiclesQuery, IEnumerable<VehiclesResponse>>
{
    public async Task<Result<IEnumerable<VehiclesResponse>>> Handle(GetVehiclesQuery query, CancellationToken cancellationToken)
    {
        var userLat = query.UserLatitude;
        var userLon = query.UserLongitude;

        var vehicles = await context.Vehicles
            .AsNoTracking()
            .Select(v => new
            {
                Vehicle = v,
                Distance =
                    (v.Coordinates.Latitude - userLat) * (v.Coordinates.Latitude - userLat) +
                    (v.Coordinates.Longitude - userLon) * (v.Coordinates.Longitude - userLon)
            })
            .OrderBy(x => x.Distance)
            .Select(x => new VehiclesResponse(
                x.Vehicle.Id,
                x.Vehicle.Chassis,
                x.Vehicle.Identifier,
                x.Vehicle.LicensePlate,
                x.Vehicle.TrackerSerialNumber,
                x.Vehicle.Color,
                x.Vehicle.VehicleType,
                new CoordinatesResponse(
                    x.Vehicle.Coordinates.Latitude,
                    x.Vehicle.Coordinates.Longitude
                )
            ))
            .ToListAsync(cancellationToken);

        return vehicles;
    }
}