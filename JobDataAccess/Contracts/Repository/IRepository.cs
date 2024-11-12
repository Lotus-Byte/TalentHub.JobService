using JobDataAccess.Entities;

namespace JobDataAccess.Contracts.Repository;

/// <summary>
/// Описания общих методов для всех репозиториев.
/// </summary>
/// <typeparam name="T"> Тип Entity для репозитория. </typeparam>
/// <typeparam name="TPrimaryKey"> Тип первичного ключа. </typeparam>
public interface IRepository<T, TPrimaryKey>
    where T : IAggregateRoot<TPrimaryKey>
{
    /// <summary>
    /// Получить сущность по Id.
    /// </summary>
    /// <param name="id"> Id сущности. </param>
    /// <returns> Cущность. </returns>
    Task<T> GetAsync(TPrimaryKey id, CancellationToken cancellationToken);

    /// <summary>
    /// Удалить сущность.
    /// </summary>
    /// <param name="id"> Id удалённой сущности. </param>
    /// <returns> Была ли сущность удалена. </returns>
    bool Delete(TPrimaryKey id);

    /// <summary>
    /// Удалить сущность.
    /// </summary>
    /// <param name="entity"> Cущность для удаления. </param>
    /// <returns> Была ли сущность удалена. </returns>
    bool Delete(T entity);

    /// <summary>
    /// Добавить в базу одну сущность.
    /// </summary>
    /// <param name="entity"> Сущность для добавления. </param>
    /// <returns> Добавленная сущность. </returns>
    Task<T> AddAsync(T entity, CancellationToken cancellationToken);

    /// <summary>
    /// Сохранить изменения.
    /// </summary>
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
