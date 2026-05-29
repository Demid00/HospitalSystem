using Hospital.Domain.Entities;
using Hospital.Domain.Repositories;
using HospitalSystem.Infrastructure.EntityFramework.Data;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystem.Infrastructure.Repositories;

public class EfMedicalRecordRepository(AppDbContext context)
    : EfRepository<MedicalRecord, Guid>(context), IMedicalRecordRepository
{
    // OwnsMany загружается автоматически, поэтому используем базовый Find/FirstOrDefault
    public override async Task<MedicalRecord?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await _dbSet.FirstOrDefaultAsync(m => m.Id == id, cancellationToken);

    public async Task<MedicalRecord?> GetByAppointmentIdAsync(Guid appointmentId, CancellationToken cancellationToken = default)
        => await _dbSet.FirstOrDefaultAsync(m => m.AppointmentId == appointmentId, cancellationToken);
}