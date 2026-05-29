using Hospital.Domain.Entities;
using Hospital.Domain.Repositories;
using HospitalSystem.Infrastructure.EntityFramework.Data;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystem.Infrastructure.Repositories;

public class EfPatientRepository(AppDbContext context)
    : EfRepository<Patient, Guid>(context), IPatientRepository
{
    // GetByIdAsync использует базовую реализацию (без Include)
    public async Task<Patient?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        => await _dbSet.FirstOrDefaultAsync(p => p.Email.Value == email, cancellationToken);
}