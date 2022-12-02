namespace Elsa.API.Domain.Common;

/// <summary>
/// Объект аудита.
/// </summary>
/// <typeparam name="TId"></typeparam>
public interface IAuditableEntity<TId> : IAuditableEntity, IEntity<TId>
{
}

/// <summary>
/// Объект аудита.
/// </summary>
public interface IAuditableEntity : IEntity
{
    /// <summary>
    /// <see langword="true"/> - объект удален и не учавствует в выборке по стандарту.
    /// </summary>
    bool IsDeleted { get; set; }

    /// <summary>
    /// Дата создания.
    /// </summary>
    DateTime CreatedOn { get; set; }

    /// <summary>
    /// Дата удаления.
    /// </summary>
    DateTime? DeletedOn { get; set; }

    /// <summary>
    /// Дата последнего изменения.
    /// </summary>
    DateTime? LastModifiedOn { get; set; }
}
