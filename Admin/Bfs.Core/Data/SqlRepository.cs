using System.Linq.Expressions;
using Bfs.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bfs.Core.Data;

/// <summary>
///     Abstract implementation of generic scoped SQL repository.
///     Search operations in such repository must rely on a composite key (Id, TenantId).
/// </summary>
/// <typeparam name="TEntity">Type of the entity.</typeparam>
/// <typeparam name="TDbContextBase">Type of the Unit of Work implementation.</typeparam>
public abstract class SqlRepository<TEntity, TDbContextBase> : IRepository<TEntity>
    where TEntity : class, IIdentifiable, ITenanted, new()
    where TDbContextBase : DbContext
{
    /// <summary>
    ///     The unit of work for direct data access.
    /// </summary>
    protected readonly DbContext DbContextBase;

    /// <summary>
    ///     Database set for the entity.
    /// </summary>
    protected readonly DbSet<TEntity> DbSet;


    /// <summary>
    ///     Creates a new instance of the <see cref="SqlRepository{TEntity, TDbContextBase}" /> class.
    /// </summary>
    /// <param name="dbContextBase">Unit of Work implementation.</param>
    /// <param name="scopeData">Scope related data.</param>
    public SqlRepository(DbContext dbContextBase, IScopeData scopeData)
    {
        ScopeData = scopeData;
        DbContextBase = dbContextBase;
        DbSet = dbContextBase.Set<TEntity>();
    }

    /// <inheritdoc />
    public IScopeData ScopeData { get; }


    /// <inheritdoc />
    public virtual async Task<TEntity> CreateAsync(TEntity entity)
    {
        entity.Id = IdGenerator.GetId();
        entity.TenantId = ScopeData.TenantId;

        await DbSet
            .AddAsync(entity)
            .ConfigureAwait(false);

        return entity;
    }

    /// <inheritdoc />
    public virtual Task DeleteAsync(TEntity entity)
    {
        // disable check for now
        //if (entity.TenantId != ScopeData.TenantId)
        //{
        //    throw new InvalidOperationException(DataError.DeleteOutOfScope);
        //}

        DbSet.Remove(entity);
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public virtual Task DeleteAsync(List<TEntity> entities)
    {
        // disable check for now
        //if (entity.TenantId != ScopeData.TenantId)
        //{
        //    throw new InvalidOperationException(DataError.DeleteOutOfScope);
        //}

        DbSet.RemoveRange(entities);
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public virtual async Task DeleteAsync(Expression<Func<TEntity, bool>> filter)
    {
        IEnumerable<TEntity> entities = await DbSet
            .Where(filter)
            .Where(e => e.TenantId == ScopeData.TenantId)
            .ToListAsync()
            .ConfigureAwait(false);

        if (entities.Any(e => e.TenantId != ScopeData.TenantId))
            throw new InvalidOperationException(DataError.DeleteOutOfScope);

        DbSet.RemoveRange(entities);
    }

    /// <inheritdoc />
    public virtual async Task<TEntity?> GetAsync(long id)
    {
        var result = await DbSet
            .FirstOrDefaultAsync(e => e.Id == id)
            .ConfigureAwait(false);
        return result;
    }

    public virtual async Task<List<TEntity>> GetAsync()
    {
        var result = await DbSet
            .ToListAsync()
            .ConfigureAwait(false);
        return result;
    }

    /// <inheritdoc />
    public virtual Task UpdateAsync(TEntity entity)
    {
        // disable check for now
        //if (entity.TenantId != ScopeData.TenantId)
        //{
        //    throw new InvalidOperationException(DataError.UpdateOutOfScope);
        //}

        DbSet.Update(entity);
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public virtual async Task<TEntity> UploadAsync(TEntity entity)
    {
        if (entity.Id == 0)
            entity.Id = IdGenerator.GetId();

        entity.Id = entity.Id; // takes the uploaded id for now. later we can decide to generate a new one.
        entity.TenantId = ScopeData.TenantId;

        await DbSet
            .AddAsync(entity)
            .ConfigureAwait(false);

        return entity;
    }

    /// <inheritdoc />
    public virtual async Task<bool> AnyAsync(
        Expression<Func<TEntity, bool>> filter)
    {
        return await DbSet
            .Where(filter)
            .Where(e => e.TenantId == ScopeData.TenantId)
            .AnyAsync()
            .ConfigureAwait(false);
    }

    /// <inheritdoc />
    public virtual async Task<long> CountAsync(
        Expression<Func<TEntity, bool>> filter)
    {
        return await DbSet
            .Where(filter)
            .Where(e => e.TenantId == ScopeData.TenantId)
            .LongCountAsync()
            .ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task SaveAsync()
    {
        var entries = DbContextBase.ChangeTracker.Entries();

        foreach (var entry in entries)
            if (entry.State == EntityState.Added
                && entry.Entity is ITenanted tenanted
                && tenanted.TenantId != ScopeData.TenantId)
                throw new InvalidOperationException(DataError.SaveOutOfScope);

        await DbContextBase
            .SaveChangesAsync()
            .ConfigureAwait(false);
    }

    /// <summary>
    ///     Get the nested objects to be included when fetching from the database.
    /// </summary>
    protected virtual List<Expression<Func<TEntity, object>>> GetIncludes()
    {
        return new List<Expression<Func<TEntity, object>>>();
    }


    /// <summary>
    ///     Execute logic in a single transaction and get result. If any of the operations in the transaction fails, the
    ///     transaction is rolled back.
    /// </summary>
    protected async Task<T> WrapTransactionAsync<T>(Func<Task<T>> func)
    {
        ArgumentNullException.ThrowIfNull(func, nameof(func));

        using var transaction = await DbContextBase.Database.BeginTransactionAsync().ConfigureAwait(false);
        try
        {
            var result = await func().ConfigureAwait(false);
            await transaction.CommitAsync().ConfigureAwait(false);
            return result;
        }
        catch
        {
            await transaction.RollbackAsync().ConfigureAwait(false);
            throw;
        }
    }

    /// <summary>
    ///     Execute logic in a single transaction. If Any of the operations in the transaction fails, the transaction is rolled
    ///     back.
    /// </summary>
    protected async Task WrapTransactionAsync(Func<Task> func)
    {
        ArgumentNullException.ThrowIfNull(func, nameof(func));

        using var transaction = await DbContextBase.Database.BeginTransactionAsync().ConfigureAwait(false);
        try
        {
            await func().ConfigureAwait(false);
            await transaction.CommitAsync().ConfigureAwait(false);
        }
        catch
        {
            await transaction.RollbackAsync().ConfigureAwait(false);
            throw;
        }
    }
}