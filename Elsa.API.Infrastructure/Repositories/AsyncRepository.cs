using Elsa.API.Application.Common.Interfaces;
using Elsa.API.Domain.Common;
using Elsa.API.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Elsa.API.Infrastructure.Repositories;

/// <inheritdoc cref="IAsyncRepository{T, TId}"/>
public class AsyncRepository<T, TId> : IAsyncRepository<T, TId> where T : Entity<TId>
{
    protected DbSet<T> Set { get; }
    protected ElsaDbContext DbContext { get; }

    /// <summary>
    /// Конструктор.
    /// </summary>
    public AsyncRepository(ElsaDbContext dbContext)
    {
        Set = dbContext.Set<T>();
        this.DbContext = dbContext;
    }

    public IQueryable<T> Entities(bool tracking)
    {
        if (tracking) return Set;
        else return Set.AsNoTracking();
    }

    public async Task<T> AddAsync(T entity, CancellationToken cancellationToken)
    {
        await Set.AddAsync(entity, cancellationToken);
        return entity;
    }

    public void Delete(T entity)
    {
        Set.Remove(entity);
    }

    public void DeleteRange(IEnumerable<T> entities)
    {
        Set.RemoveRange(entities);
    }

    public Task<List<T>> GetAllAsync()
    {
        return Set.ToListAsync();
    }

    public async Task<T?> GetByIdAsync(TId id)
    {
        return await Set.FindAsync(id);
    }

    public Task UpdateAsync(T entity)
    {
        Set.Update(entity);
        return Task.CompletedTask;
        //T exist = await set.FindAsync(entity.Id);
        //dbContext.Entry(exist).CurrentValues.SetValues(entity);
    }

    public Task UpdateRangeAsync(IEnumerable<T> entities)
    {
        Set.UpdateRange(entities);
        return Task.CompletedTask;
    }
}
