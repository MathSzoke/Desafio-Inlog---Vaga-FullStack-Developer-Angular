using Inlog.Desafio.Backend.WebApi.Middleware;

namespace Inlog.Desafio.Backend.WebApi.Extensions;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseRequestContextLogging(this IApplicationBuilder app)
    {
        app.UseMiddleware<RequestContextLoggingMiddleware>();

        return app;
    }
}