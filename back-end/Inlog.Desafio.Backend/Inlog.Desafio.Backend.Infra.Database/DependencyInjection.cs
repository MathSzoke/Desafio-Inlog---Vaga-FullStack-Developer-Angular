using Inlog.Desafio.Backend.Application.Abstractions.Data;
using Inlog.Desafio.Backend.Infra.Database.Database.Contexts;
using Inlog.Desafio.Backend.Infra.Database.Time;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel;
using SharedKernel.DomainEvents;

namespace Inlog.Desafio.Backend.Infra.Database;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        return services
            .AddServices(configuration)
            .AddHealthChecks(configuration);
    }

    private static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("inlogDB");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddScoped<IApplicationDbContext>(provider =>
            provider.GetRequiredService<ApplicationDbContext>());

        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddTransient<IDomainEventsDispatcher, DomainEventsDispatcher>();

        return services;
    }

    private static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("inlogDB");
        services.AddHealthChecks().AddNpgSql(connectionString!, name: "PostgreSQL");

        return services;
    }
}