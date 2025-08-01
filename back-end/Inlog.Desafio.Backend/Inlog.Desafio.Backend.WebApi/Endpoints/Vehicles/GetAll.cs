using Inlog.Desafio.Backend.Application.Messaging;
using Inlog.Desafio.Backend.Application.Services.Vehicles.GetAll;
using Inlog.Desafio.Backend.WebApi.Extensions;
using Inlog.Desafio.Backend.WebApi.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Inlog.Desafio.Backend.WebApi.Endpoints.Vehicles;

internal sealed class GetAll : IEndpoint
{
    // TODO: retornar todos veiculos 
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("api/v1/Veiculos/Listar", async (
                [FromQuery]double userLatitude,
                [FromQuery]double userLongitude,
                IQueryHandler<GetVehiclesQuery, IEnumerable<VehiclesResponse>> handler,
                CancellationToken cancellationToken) =>
            {
                var query = new GetVehiclesQuery(userLatitude, userLongitude);
                var result = await handler.Handle(query, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .WithTags(Tags.Vehicles);
    }
}