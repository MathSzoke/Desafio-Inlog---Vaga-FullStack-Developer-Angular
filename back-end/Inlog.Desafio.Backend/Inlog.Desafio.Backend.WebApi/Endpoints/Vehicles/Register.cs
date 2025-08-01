using Inlog.Desafio.Backend.Application.Messaging;
using Inlog.Desafio.Backend.Application.Services.Vehicles.Register;
using Inlog.Desafio.Backend.Domain.Vehicles;
using Inlog.Desafio.Backend.WebApi.Extensions;
using Inlog.Desafio.Backend.WebApi.Infrastructure;

namespace Inlog.Desafio.Backend.WebApi.Endpoints.Vehicles;

internal sealed class Register : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("api/v1/Veiculos/Cadastrar", async (
                Request request,
                ICommandHandler<RegisterVehicleCommand, Vehicle> handler,
                CancellationToken cancellationToken) =>
            {
                var command = new RegisterVehicleCommand(
                    request.Identifier,
                    request.Chassis,
                    request.LicensePlate,
                    request.TrackerSerialNumber,
                    request.VehicleType,
                    request.Color,
                    request.Coordinates
                );

                var result = await handler.Handle(command, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .WithTags(Tags.Vehicles);
    }

    private sealed record Request(
        string Identifier,
        string Chassis,
        string LicensePlate,
        string TrackerSerialNumber,
        VehicleType VehicleType,
        string Color,
        Coordinates Coordinates
    );
}