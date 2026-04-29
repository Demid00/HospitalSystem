using Hospital.Domain.Entities;

namespace Hospital.Domain.Repositories;

public interface IDoctorRepository : IRepository<Doctor, Guid>
{
    Task<Doctor?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    Task<IEnumerable<Doctor>> GetActiveDoctorsAsync(CancellationToken cancellationToken);
    Task<IEnumerable<Doctor>> GetBySpecializationAsync(string specialization, CancellationToken cancellationToken);
}