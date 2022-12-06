using Elsa.API.Domain.Common;

namespace Elsa.API.Application.Common.Interfaces;

/// <summary>
/// Репозиторий.
/// </summary>
public interface IAsyncRepository<T, in TId> where T : class, IEntity<TId>
{
    /// <summary>
    /// Сущности.
    /// </summary>
    IQueryable<T> Entities(bool tracking = false);

    /// <summary>
    /// Получить сущность по Id.
    /// </summary>
    /// <param name="id">Id.</param>
    /// <returns></returns>
    Task<T?> GetByIdAsync(TId id);

    /// <summary>
    /// Получить все сущности.
    /// </summary>
    /// <returns></returns>
    Task<List<T>> GetAllAsync();

    /// <summary>
    /// Добавить сущность.
    /// </summary>
    /// <param name="entity">Новая сущность.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns></returns>
    Task<T> AddAsync(T entity, CancellationToken cancellationToken);

    /// <summary>
    /// Обновить сущность.
    /// </summary>
    /// <param name="entity">Сущность, для обновления.</param>
    /// <returns></returns>
    Task UpdateAsync(T entity);

    /// <summary>
    /// Обновить сущности.
    /// </summary>
    /// <param name="entities">Сущности.</param>
    /// <returns></returns>
    Task UpdateRangeAsync(IEnumerable<T> entities);

    /// <summary>
    /// Удалить сущность.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <returns></returns>
    void Delete(T entity);

    /// <summary>
    /// Удаление списка сущностей.
    /// </summary>
    /// <param name="entities">Сущности.</param>
    /// <returns></returns>
    void DeleteRange(IEnumerable<T> entities);
}
