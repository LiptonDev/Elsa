using Elsa.API.Domain.Common;
using MediatR;

namespace Elsa.API.Application.Common.Models;

/// <summary>
/// Оповещение об изменении сущности.
/// </summary>
/// <typeparam name="TDomainEvent"></typeparam>
public class DomainEventNotification<TDomainEvent> : INotification where TDomainEvent : DomainEvent
{
    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="domainEvent"></param>
	public DomainEventNotification(TDomainEvent domainEvent)
    {
        DomainEvent = domainEvent;
    }

    /// <summary>
    /// Событие.
    /// </summary>
    public TDomainEvent DomainEvent { get; }
}
