using Hospital.Domain.Entities;

namespace Hospital.Domain.Repositories;

public interface IDoctorRepository : IRepository<Doctor, Guid>
{
    Task<Doctor?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    Task<IReadOnlyList<Doctor>> GetActiveDoctorsAsync(CancellationToken cancellationToken);
    Task<IReadOnlyList<Doctor>> GetBySpecializationAsync(string specialization, CancellationToken cancellationToken);
}