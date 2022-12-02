namespace Elsa.API.Domain.Common;

/// <summary>
/// Entity.
/// </summary>
/// <typeparam name="TId"></typeparam>
public interface IEntity<TId> : IEntity
{
    /// <summary>
    /// Id.
    /// </summary>
    public TId Id { get; set; }
}

/// <summary>
/// Entity.
/// </summary>
public interface IEntity
{
}
