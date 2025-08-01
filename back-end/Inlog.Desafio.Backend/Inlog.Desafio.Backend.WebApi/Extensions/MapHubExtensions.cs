using Inlog.Desafio.Backend.WebApi.Infrastructure.Hubs;

namespace Inlog.Desafio.Backend.WebApi.Extensions;

public static class MapHubExtensions
{
    public static IApplicationBuilder MapHubs(this WebApplication app)
    {
        app.MapHub<VehicleHub>("/hub/vehicles");

        return app;
    }
}