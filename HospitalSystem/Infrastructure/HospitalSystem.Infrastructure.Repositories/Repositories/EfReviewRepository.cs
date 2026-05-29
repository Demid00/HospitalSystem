using Hospital.Domain.Entities;
using Hospital.Domain.Repositories;
using HospitalSystem.Infrastructure.EntityFramework.Data;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystem.Infrastructure.Repositories;

public class EfReviewRepository(AppDbContext context)
    : EfRepository<Review, Guid>(context), IReviewRepository
{
    public async Task<IReadOnlyList<Review>> GetByDoctorIdAsync(Guid doctorId, CancellationToken cancellationToken = default)
        => await _dbSet
            .Where(r => r.DoctorId == doctorId)
            .ToListAsync(cancellationToken);
}