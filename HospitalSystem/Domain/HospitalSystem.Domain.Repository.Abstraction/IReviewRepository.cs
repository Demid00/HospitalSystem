using Hospital.Domain.Entities;

namespace Hospital.Domain.Repositories;

public interface IReviewRepository : IRepository<Review, Guid>
{
    Task<IReadOnlyList<Review>> GetByDoctorIdAsync(Guid doctorId, CancellationToken cancellationToken);
}