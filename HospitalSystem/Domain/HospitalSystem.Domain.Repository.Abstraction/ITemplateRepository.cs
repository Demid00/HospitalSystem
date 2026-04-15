// ITemplateRepository.cs
using Hospital.Domain.Repositories.Abstractions.Base;
using Hospital.Domain.Entities;

namespace Hospital.Domain.Repositories.Abstractions;

public interface ITemplateRepository : IRepository<Template, Guid>
{
    Task<IReadOnlyList<Template>> GetByDoctorIdAsync(Guid doctorId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Template>> GetActiveByDoctorIdAsync(Guid doctorId, CancellationToken cancellationToken = default);
}