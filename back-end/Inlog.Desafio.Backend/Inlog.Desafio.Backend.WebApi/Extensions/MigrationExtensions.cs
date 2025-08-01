using Inlog.Desafio.Backend.Infra.Database.Database.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Inlog.Desafio.Backend.WebApi.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var serviceProvider = scope.ServiceProvider;

        var appDbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
        appDbContext.Database.Migrate();
    }
}