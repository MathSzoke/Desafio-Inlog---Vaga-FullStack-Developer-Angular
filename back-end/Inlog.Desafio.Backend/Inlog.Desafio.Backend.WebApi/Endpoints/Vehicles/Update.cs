using Inlog.Desafio.Backend.Application.Messaging;
using Inlog.Desafio.Backend.Application.Services.Vehicles.Update;
using Inlog.Desafio.Backend.WebApi.Extensions;
using Inlog.Desafio.Backend.WebApi.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Inlog.Desafio.Backend.WebApi.Endpoints.Vehicles;

internal sealed class Update : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("api/v1/Veiculos/Atualizar", async (
            [FromBody] UpdateVehicleCommand command,
            ICommandHandler<UpdateVehicleCommand> handler,
            CancellationToken cancellationToken) =>
        {
            var result = await handler.Handle(command, cancellationToken);

            return result.Match(
                Results.NoContent,
                CustomResults.Problem);
        })
        .WithTags(Tags.Vehicles);
        // .RequireAuthorization(); // Como não possuímos usuário para autenticação, essa possibilidade de exigir authorization não se encaixa no nosso contexto.
    }
}