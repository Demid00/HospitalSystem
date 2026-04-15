// IProcedureRepository.cs
using Hospital.Domain.Repositories.Abstractions.Base;
using Hospital.Domain.Entities;

namespace Hospital.Domain.Repositories.Abstractions;

public interface IProcedureRepository : IRepository<Procedure, Guid>
{
    Task<IReadOnlyList<Procedure>> GetActiveProceduresAsync(CancellationToken cancellationToken = default);
    Task<Procedure?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
}