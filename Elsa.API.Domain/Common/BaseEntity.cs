namespace Elsa.API.Domain.Common;

/// <summary>
/// Базовая сущность.
/// </summary>
public class BaseEntity
{
    /// <summary>
    /// Id сущности.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// <see langword="true"/> - запись удалена (soft deleting).
    /// </summary>
    public bool IsDeleted { get; set; }

    /// <summary>
    /// Дата удаления.
    /// </summary>
    public DateTime? DeleteDate { get; set; }

    /// <summary>
    /// Дата создания.
    /// </summary>
    public DateTime CreateDate { get; set; }
}
