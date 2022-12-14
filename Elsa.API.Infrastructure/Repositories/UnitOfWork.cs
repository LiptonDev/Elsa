using Elsa.API.Application.Common.Interfaces;
using Elsa.API.Domain.Common;
using Elsa.API.Infrastructure.Persistence;
using System.Collections;

namespace Elsa.API.Infrastructure.Repositories;

/// <inheritdoc cref="IUnitOfWork{TId}"/>
public class UnitOfWork<TId> : IUnitOfWork<TId>
{
    private bool disposed;
    private readonly Hashtable repositories;
    private readonly ElsaDbContext dbContext;

    /// <summary>
    /// Конструктор.
    /// </summary>
    public UnitOfWork(ElsaDbContext dbContext)
    {
        this.dbContext = dbContext;
        repositories = new Hashtable();
    }

    public IAsyncRepository<TEntity, TId> Repository<TEntity>() where TEntity : Entity<TId>
    {
        var type = typeof(TEntity).Name;

        if (!repositories.ContainsKey(type))
        {
            var repositoryType = typeof(AsyncRepository<,>);

            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity), typeof(TId)), dbContext);

            repositories.Add(type, repositoryInstance);
        }

        return (IAsyncRepository<TEntity, TId>)repositories[type]!;
    }

    public Task<int> Commit(CancellationToken cancellationToken)
    {
        return dbContext.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                dbContext.Dispose();
            }
        }
        disposed = true;
    }
}
