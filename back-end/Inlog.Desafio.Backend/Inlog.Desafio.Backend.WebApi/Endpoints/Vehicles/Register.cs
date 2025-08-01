using Inlog.Desafio.Backend.Application.Messaging;
using Inlog.Desafio.Backend.WebApi.Extensions;

namespace Inlog.Desafio.Backend.WebApi.Endpoints.Vehicles;

internal sealed class Add : IEndpoint
{
    // TODO: Cadastrar um veiculo em memoria ou banco de dados
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("api/v1/Veiculos/Cadastrar", async (
                Request request,
                ICommandHandler<RegisterVehicleCommand, RegisterResponse> handler,
                CancellationToken cancellationToken) =>
            {
                var command = new RegisterVehicleCommand(
                    request.Email,
                    request.Name,
                    request.Password);

                var result = await handler.Handle(command, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .WithTags(Tags.Vehicles);
    }
    private sealed record Request(string Email, string Name, string Password);
}