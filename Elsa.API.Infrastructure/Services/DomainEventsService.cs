using Elsa.API.Application.Common.Interfaces;
using Elsa.API.Application.Common.Models;
using Elsa.API.Domain.Common;
using MediatR;

namespace Elsa.API.Infrastructure.Services;

/// <inheritdoc cref="IDomainEventsService"/>
public class DomainEventsService : IDomainEventsService
{
    private readonly IPublisher publisher;

    /// <summary>
    /// Конструктор.
    /// </summary>
    public DomainEventsService(IPublisher publisher)
    {
        this.publisher = publisher;
    }

    public Task PublishAsync(DomainEvent domainEvent, CancellationToken cancellationToken)
    {
        return publisher.Publish(GetNotification(domainEvent), cancellationToken);
    }

    private INotification GetNotification(DomainEvent domainEvent)
    {
        return (INotification)Activator.CreateInstance(typeof(DomainEventNotification<>).MakeGenericType(domainEvent.GetType()), domainEvent)!;
    }
}
