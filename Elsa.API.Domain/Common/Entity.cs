namespace Elsa.API.Domain.Common
{
    /// <inheritdoc cref="IEntity{TId}"/>
    public abstract class Entity<TId> : IEntity<TId>, IEntity
    {
        public TId Id { get; set; }
    }
}
