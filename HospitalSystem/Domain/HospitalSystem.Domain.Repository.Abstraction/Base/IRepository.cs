using Hospital.Domain.Base;

namespace Hospital.Domain.Repositories;

public interface IRepository<TEntity, in TId>
    where TEntity : Entity<TId>
    where TId : struct, IEquatable<TId>
{
    Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken, bool asNoTracking = false);
    Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken);
    Task AddAsync(TEntity entity, CancellationToken cancellationToken);
    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken);
    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken);
    Task DeleteByIdAsync(TId id, CancellationToken cancellationToken);
}