using Microsoft.Extensions.DependencyInjection;

namespace SharedKernel.DomainEvents;

public sealed class DomainEventsDispatcher(IServiceProvider serviceProvider) : IDomainEventsDispatcher
{
    public async Task DispatchAsync(
        IEnumerable<IDomainEvent> domainEvents,
        CancellationToken cancellationToken = default)
    {
        foreach (var domainEvent in domainEvents)
        {
            using var scope = serviceProvider.CreateScope();

            var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(domainEvent.GetType());
            var handlers = (IEnumerable<object>?)scope.ServiceProvider.GetService(
                typeof(IEnumerable<>).MakeGenericType(handlerType));

            foreach (var handler in handlers ?? new List<object>())
            {
                var handleMethod = handlerType.GetMethod("Handle");
                if (handleMethod != null) await (Task)handleMethod.Invoke(handler, new object?[]{domainEvent, cancellationToken})!;
            }
        }
    }
}