// IPatientRepository.cs
using Hospital.Domain.Repositories.Abstractions.Base;
using Hospital.Domain.Entities;

namespace Hospital.Domain.Repositories.Abstractions;

public interface IPatientRepository : IRepository<Patient, Guid>
{
    Task<Patient?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Patient>> GetActivePatientsAsync(CancellationToken cancellationToken = default);
    Task<Patient?> GetWithAppointmentsAsync(Guid patientId, CancellationToken cancellationToken = default);
}