using Hospital.Domain.Entities;
using Hospital.Domain.Repositories;
using HospitalSystem.Infrastructure.EntityFramework.Data;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystem.Infrastructure.Repositories;

public class EfAppointmentRepository(AppDbContext context)
    : EfRepository<Appointment, Guid>(context), IAppointmentRepository
{
    public async Task<IReadOnlyList<Appointment>> GetByDoctorAndDateAsync(Guid doctorId, DateTime date, CancellationToken cancellationToken = default)
        => await _dbSet
            .Where(a => a.DoctorId == doctorId && a.DateTime.Value.Date == date.Date)
            .ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<Appointment>> GetByPatientAsync(Guid patientId, CancellationToken cancellationToken = default)
        => await _dbSet
            .Where(a => a.PatientId == patientId)
            .ToListAsync(cancellationToken);
}