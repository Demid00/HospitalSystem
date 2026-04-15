// Base/IRepository.cs
using Hospital.Domain.Base;

namespace Hospital.Domain.Repositories.Abstractions.Base;

public interface IRepository<TEntity, in TId>
    where TEntity : Entity<TId>
    where TId : struct, IEquatable<TId>
{
    Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(TId id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(TId id, CancellationToken cancellationToken = default);
}