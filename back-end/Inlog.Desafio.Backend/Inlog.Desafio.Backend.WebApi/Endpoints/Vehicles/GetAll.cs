using Inlog.Desafio.Backend.Application.Messaging;
using Inlog.Desafio.Backend.WebApi.Extensions;

namespace Inlog.Desafio.Backend.WebApi.Endpoints.Veiculos;

internal sealed class GetAll : IEndpoint
{
    // TODO: retornar todos veiculos 
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("api/v1/Veiculos/Listar", async (
                IQueryHandler<GetVehiclesQuery, List<VehicleResponse>> handler,
                CancellationToken cancellationToken) =>
            {
                var query = new GetVehiclesQuery();
                var result = await handler.Handle(query, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .WithTags(Tags.Vehicles);
    }
}