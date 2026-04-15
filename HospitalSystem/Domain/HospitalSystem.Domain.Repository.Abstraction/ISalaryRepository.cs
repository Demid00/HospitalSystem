// ISalaryRepository.cs
using Hospital.Domain.Repositories.Abstractions.Base;
using Hospital.Domain.Entities;

namespace Hospital.Domain.Repositories.Abstractions;

public interface ISalaryRepository : IRepository<Salary, Guid>
{
    Task<IReadOnlyList<Salary>> GetByDoctorIdAsync(Guid doctorId, CancellationToken cancellationToken = default);
    Task<Salary?> GetByDoctorAndPeriodAsync(Guid doctorId, int year, int month, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Salary>> GetUnpaidSalariesAsync(CancellationToken cancellationToken = default);
}