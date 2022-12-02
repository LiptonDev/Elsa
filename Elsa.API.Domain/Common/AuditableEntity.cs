namespace Elsa.API.Domain.Common;

/// <inheritdoc cref="IAuditableEntity{TId}"/>
public abstract class AuditableEntity<TId> : Entity<TId>, IAuditableEntity<TId>
{
    public bool IsDeleted { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? DeletedOn { get; set; }
    public DateTime? LastModifiedOn { get; set; }
}
