using Hospital.Domain.Entities;
using Hospital.Domain.Repositories;
using HospitalSystem.Infrastructure.EntityFramework.Data;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystem.Infrastructure.Repositories;

public class EfPatientRepository(AppDbContext context)
    : EfRepository<Patient, Guid>(context), IPatientRepository
{
    public override async Task<Patient?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await _dbSet
            .Include("_appointments")
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

    public async Task<Patient?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        => await _dbSet.FirstOrDefaultAsync(p => p.Email.Value == email, cancellationToken);
}