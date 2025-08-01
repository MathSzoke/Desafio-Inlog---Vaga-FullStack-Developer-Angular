namespace Inlog.Desafio.Backend.WebApi.Extensions;

public static class CorsExtensions
{
    public static IServiceCollection AddCorsServices(this IServiceCollection services, IConfiguration configuration)
    {
        var frontendUrl = configuration["INLOG_FRONTEND"];

        services.AddCors(options =>
        {
            options.AddPolicy("AllowMyFrontend", policy =>
            {
                policy.WithOrigins(frontendUrl!)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });

        return services;
    }
}