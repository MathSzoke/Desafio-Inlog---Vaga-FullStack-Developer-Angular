using Microsoft.AspNetCore.Routing;

namespace Inlog.Desafio.Backend.WebApi.Endpoints;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}