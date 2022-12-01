namespace Elsa.API.Domain.Common;

/// <summary>
/// Указывает, что сущность имеет события измнения.
/// </summary>
public interface IHasDomainEvents
{
    /// <summary>
    /// События.
    /// </summary>
    List<DomainEvent> DomainEvents { get; }
}
