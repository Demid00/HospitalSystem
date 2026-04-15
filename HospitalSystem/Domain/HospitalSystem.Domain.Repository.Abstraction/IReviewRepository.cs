// IReviewRepository.cs
using Hospital.Domain.Repositories.Abstractions.Base;
using Hospital.Domain.Entities;

namespace Hospital.Domain.Repositories.Abstractions;

public interface IReviewRepository : IRepository<Review, Guid>
{
    Task<IReadOnlyList<Review>> GetByDoctorIdAsync(Guid doctorId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Review>> GetByPatientIdAsync(Guid patientId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Review>> GetPendingApprovalAsync(CancellationToken cancellationToken = default);
    Task<double> GetAverageRatingForDoctorAsync(Guid doctorId, CancellationToken cancellationToken = default);
}