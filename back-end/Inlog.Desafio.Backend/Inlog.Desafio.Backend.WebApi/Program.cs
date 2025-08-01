using System.Reflection;
using Inlog.Desafio.Backend.Application;
using Inlog.Desafio.Backend.Infra.Database;
using Inlog.Desafio.Backend.WebApi;
using Inlog.Desafio.Backend.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddPresentation()
    .AddInfrastructure(builder.Configuration);

builder.Services.AddCorsServices(builder.Configuration);

builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());

builder.Services.AddHttpClient();

var app = builder.Build();

app.UseCors("AllowMyFrontend");

app.MapEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerWithUi();

    app.ApplyMigrations();
}

app.UseRequestContextLogging();

app.MapHubs();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();

namespace Inlog.Desafio.Backend.WebApi
{
    public class Program { }
}