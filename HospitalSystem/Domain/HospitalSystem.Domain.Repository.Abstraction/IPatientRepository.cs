using Hospital.Domain.Entities;

namespace Hospital.Domain.Repositories;

public interface IPatientRepository : IRepository<Patient, Guid>
{
    Task<Patient?> GetByEmailAsync(string email, CancellationToken cancellationToken);
}