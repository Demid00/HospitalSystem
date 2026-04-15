// IUserRepository.cs
using Hospital.Domain.Repositories.Abstractions.Base;
using Hospital.ValueObjects;
using Hospital.Domain.Entities;
using Hospital.Domain.Enums;

namespace Hospital.Domain.Repositories.Abstractions;

public interface IUserRepository : IRepository<User, Guid>
{
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<User?> GetByEmailAsync(Email email, CancellationToken cancellationToken = default);
    Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<User>> GetByRoleAsync(UserRole role, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<User>> GetActiveUsersAsync(CancellationToken cancellationToken = default);
}