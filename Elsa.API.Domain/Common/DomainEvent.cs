namespace Elsa.API.Domain.Common;

/// <summary>
/// Событие сущности.
/// </summary>
public abstract class DomainEvent
{
    /// <summary>
    /// Конструктор.
    /// </summary>
    protected DomainEvent()
    {
        DateOccurred = DateTime.UtcNow;
    }

    /// <summary>
    /// Дата и время, когда событие произошло.
    /// </summary>
    public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;

    /// <summary>
    /// Статус события.
    /// </summary>
    public bool IsPublished { get; set; }
}
