using Hospital.Domain.Entities;
using Hospital.Domain.Repositories;
using HospitalSystem.Infrastructure.EntityFramework.Data;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystem.Infrastructure.Repositories;

public class EfDoctorRepository(AppDbContext context)
    : EfRepository<Doctor, Guid>(context), IDoctorRepository
{
    // GetByIdAsync использует базовую реализацию
    public async Task<Doctor?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        => await _dbSet.FirstOrDefaultAsync(d => d.Email.Value == email, cancellationToken);

    public async Task<IReadOnlyList<Doctor>> GetActiveDoctorsAsync(CancellationToken cancellationToken = default)
        => await _dbSet.Where(d => d.IsActive).ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<Doctor>> GetBySpecializationAsync(string specialization, CancellationToken cancellationToken = default)
        => await _dbSet.Where(d => d.Specialization.Value == specialization).ToListAsync(cancellationToken);
}