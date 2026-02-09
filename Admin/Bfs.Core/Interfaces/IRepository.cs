using System.Linq.Expressions;

namespace Bfs.Core.Interfaces;

/// <summary>
///     Generic data repository.
/// </summary>
/// <typeparam name="TEntity">Entity type.</typeparam>
public interface IRepository<TEntity> where TEntity : class, IIdentifiable, ITenanted, new()
{
    /// <summary>
    ///     The data of the current scope.
    /// </summary>
    IScopeData ScopeData { get; }

    /// <summary>
    ///     Saves any state changes.
    /// </summary>
    /// <returns>Task object.</returns>
    Task SaveAsync();

    /// <summary>
    ///     Retrieve an entity by ID.
    /// </summary>
    /// <param name="id">Entity ID.</param>
    /// <returns>Entity.</returns>
    Task<TEntity?> GetAsync(long id);

    /// <summary>
    ///     Retrieve a page of entities based on a query.
    /// </summary>
    /// <param name="queryRequest">Query request.</param>
    /// <returns>Paged result.</returns>
    Task<List<TEntity>> GetAsync();

    /// <summary>
    ///     Create an entity without saving.It generates a new Id for the entity.
    /// </summary>
    /// <param name="entity">Entity to be created.</param>
    /// <returns>Task object.</returns>
    Task<TEntity> CreateAsync(TEntity entity);

    /// <summary>
    ///     Update an entity without saving.
    /// </summary>
    /// <param name="entity">Entity to be updated.</param>
    /// <returns>Task object.</returns>
    Task UpdateAsync(TEntity entity);

    /// <summary>
    ///     Delete an already attached entity without saving.
    /// </summary>
    /// <param name="entity">Attached entity to be deleted.</param>
    /// <returns>Task object.</returns>
    Task DeleteAsync(TEntity entity);

    /// <summary>
    ///     Delete a list of already attached entities without saving.
    /// </summary>
    /// <param name="entities">List of attached entities to be deleted.</param>
    /// <returns>Task object.</returns>
    Task DeleteAsync(List<TEntity> entities);

    /// <summary>
    ///     Delete an entity based on a filter.
    /// </summary>
    /// <param name="filter">Filtering predicate.</param>
    /// <returns>Task object.</returns>
    Task DeleteAsync(Expression<Func<TEntity, bool>> filter);

    /// <summary>
    ///     Creates an entity without saving using the provided Id in the entity.
    /// </summary>
    /// <param name="entity">Entity to be created.</param>
    /// <returns>Task object.</returns>
    Task<TEntity> UploadAsync(TEntity entity);

    /// <summary>
    ///     Checks if there are any records in the database matching the filter conditions.
    /// </summary>
    /// <param name="filter">Filter conditions.</param>
    /// <returns>A boolean indicating if there was a match or not.</returns>
    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter);

    /// <summary>
    ///     Counts records in the database matching the filter conditions.
    /// </summary>
    /// <param name="filter">Filter conditions.</param>
    /// <returns>Number indicating the count results.</returns>
    Task<long> CountAsync(Expression<Func<TEntity, bool>> filter);
}