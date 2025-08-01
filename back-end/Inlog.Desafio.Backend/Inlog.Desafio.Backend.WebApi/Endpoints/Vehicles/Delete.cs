using Inlog.Desafio.Backend.Application.Messaging;
using Inlog.Desafio.Backend.Application.Services.Vehicles.Delete;
using Inlog.Desafio.Backend.WebApi.Extensions;
using Inlog.Desafio.Backend.WebApi.Infrastructure;

namespace Inlog.Desafio.Backend.WebApi.Endpoints.Vehicles;

internal sealed class Delete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("api/v1/Veiculos/Remover", async (
                Request request,
                ICommandHandler<DeleteVehicleCommand> handler,
                CancellationToken cancellationToken) =>
            {
                var command = new DeleteVehicleCommand(
                    request.Id
                );

                var result = await handler.Handle(command, cancellationToken);

                return result.Match(
                    Results.NoContent,
                    CustomResults.Problem);
            })
            .WithTags(Tags.Vehicles);
    }

    private sealed record Request(
        Guid Id
    );
}