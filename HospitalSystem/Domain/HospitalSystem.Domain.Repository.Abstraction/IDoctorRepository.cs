// IDoctorRepository.cs
using Hospital.Domain.Repositories.Abstractions.Base;
using Hospital.ValueObjects;
using System.Numerics;
using Hospital.Domain.Entities;

namespace Hospital.Domain.Repositories.Abstractions;

public interface IDoctorRepository : IRepository<Doctor, Guid>
{
    Task<IReadOnlyList<Doctor>> GetBySpecializationAsync(string specialization, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Doctor>> GetBySpecializationAsync(Specialization specialization, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Doctor>> GetActiveDoctorsAsync(CancellationToken cancellationToken = default);
    Task<Doctor?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Doctor>> GetAvailableDoctorsAsync(DateTime dateTime, CancellationToken cancellationToken = default);
}