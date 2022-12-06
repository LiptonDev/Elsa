using Elsa.API.Domain.Common;

namespace Elsa.API.Application.Common.Interfaces;

/// <summary>
/// Единица работы.
/// </summary>
public interface IUnitOfWork<TId> : IDisposable
{
    /// <summary>
    /// Получить репозиторий.
    /// </summary>
    /// <returns></returns>
    IAsyncRepository<T, TId> Repository<T>() where T : Entity<TId>;

    /// <summary>
    /// Применить изменения.
    /// </summary>
    /// <returns></returns>
    Task<int> Commit(CancellationToken cancellationToken);
}
