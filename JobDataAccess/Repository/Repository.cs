using JobDataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobDataAccess.Repository;

/// <summary>
/// Репозиторий.
/// </summary>
/// <typeparam name="T"> Тип сущности. </typeparam>
/// <typeparam name="TPrimaryKey"> Тип первичного ключа. </typeparam>
public abstract class Repository<T, TPrimaryKey> where T
    : class, IAggregateRoot<TPrimaryKey>
{
    protected readonly DbContext Context;

    private readonly DbSet<T> _entitySet;

    protected Repository(DbContext context)
    {
        Context = context;
        _entitySet = Context.Set<T>();
    }

    /// <summary>
    /// Получить сущность по Id.
    /// </summary>
    /// <param name="id"> Id сущности. </param>
    /// <returns> Cущность. </returns>
    public virtual async Task<T?> GetAsync(TPrimaryKey id, CancellationToken cancellationToken)
    {
        return await _entitySet.FindAsync((object)id, cancellationToken);
    }

    /// <summary>
    /// Удалить сущность.
    /// </summary>
    /// <param name="id"> Id удалённой сущности. </param>
    /// <returns> Была ли сущность удалена. </returns>
    public virtual bool Delete(TPrimaryKey id)
    {
        var obj = _entitySet.Find(id);
        if (obj == null)
        {
            return false;
        }
        _entitySet.Remove(obj);
        return true;
    }

    /// <summary>
    /// Удалить сущность.
    /// </summary>
    /// <param name="entity"> Сущность для удаления. </param>
    /// <returns> Была ли сущность удалена. </returns>
    public virtual bool Delete(T entity)
    {
        if (entity == null)
        {
            return false;
        }
        Context.Entry(entity).State = EntityState.Deleted;
        return true;
    }

    /// <summary>
    /// Добавить в базу одну сущность.
    /// </summary>
    /// <param name="entity"> Сущность для добавления. </param>
    /// <returns> Добавленная сущность. </returns>
    public virtual async Task<T> AddAsync(T entity, CancellationToken cancellationToken)
    {
        return (await _entitySet.AddAsync(entity, cancellationToken)).Entity;
    }

    /// <summary>
    /// Сохранить изменения.
    /// </summary>
    public virtual async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await Context.SaveChangesAsync(cancellationToken);
    }
}
