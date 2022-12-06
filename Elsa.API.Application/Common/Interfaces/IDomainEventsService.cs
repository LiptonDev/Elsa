using Elsa.API.Domain.Common;

namespace Elsa.API.Application.Common.Interfaces;

/// <summary>
/// Сервис оповещения об изменениях сущностей.
/// </summary>
public interface IDomainEventsService
{
    /// <summary>
    /// Опубликовать событие.
    /// </summary>
    /// <param name="domainEvent">Событие.</param>
    /// <returns></returns>
    Task PublishAsync(DomainEvent domainEvent, CancellationToken cancellationToken);
}
