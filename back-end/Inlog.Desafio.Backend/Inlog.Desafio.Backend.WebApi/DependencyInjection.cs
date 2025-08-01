using Inlog.Desafio.Backend.Application.Abstractions;
using Inlog.Desafio.Backend.Application.Abstractions.Data;
using Inlog.Desafio.Backend.Infra.Database.Database.Contexts;
using Inlog.Desafio.Backend.WebApi.Infrastructure;
using Inlog.Desafio.Backend.WebApi.Infrastructure.RealTime;
using SharedKernel;
using SharedKernel.CurrentUser;

namespace Inlog.Desafio.Backend.WebApi;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        // REMARK: If you want to use Controllers, you'll need this.
        services.AddControllers();

        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();

        services.AddHttpClient();
        services.AddHttpContextAccessor();

        services.AddServices();

        services.AddHubsSignalR();

        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddDatabase();

        services.AddSingleton<ICurrentUserContext, CurrentUserContext>();

        return services;
    }

    private static IServiceCollection AddHubsSignalR(this IServiceCollection services)
    {
        services.AddSignalR();
        services.AddScoped<IRealTimeVehicleNotifier, VehicleNotifier>();
        
        return services;
    } 
}