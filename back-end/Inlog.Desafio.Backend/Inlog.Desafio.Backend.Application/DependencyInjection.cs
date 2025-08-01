using FluentValidation;
using Inlog.Desafio.Backend.Application.Behaviors;
using Inlog.Desafio.Backend.Application.Messaging;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel;
using Scrutor;

namespace Inlog.Desafio.Backend.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Handlers
        services.Scan(scan => scan
            .FromAssembliesOf(typeof(DependencyInjection))
            .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            .AddClasses(classes => classes.AssignableTo(typeof(IDomainEventHandler<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

        // Decorators
        TryDecorate(services, typeof(ICommandHandler<,>), typeof(ValidationDecorator.CommandHandler<,>));
        TryDecorate(services, typeof(ICommandHandler<>), typeof(ValidationDecorator.CommandBaseHandler<>));
        TryDecorate(services, typeof(IQueryHandler<,>), typeof(LoggingDecorator.QueryHandler<,>));
        TryDecorate(services, typeof(ICommandHandler<,>), typeof(LoggingDecorator.CommandHandler<,>));
        TryDecorate(services, typeof(ICommandHandler<>), typeof(LoggingDecorator.CommandBaseHandler<>));

        // Validators
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly, includeInternalTypes: true);

        return services;
    }

    private static void TryDecorate(IServiceCollection services, Type serviceType, Type decoratorType)
    {
        try
        {
            services.Decorate(serviceType, decoratorType);
        }
        catch (DecorationException)
        {
            // Ignora se não há implementações registradas
        }
    }
}
