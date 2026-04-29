using Hospital.Domain.Entities;

namespace Hospital.Domain.Repositories;

public interface IMedicalRecordRepository : IRepository<MedicalRecord, Guid>
{
    Task<MedicalRecord?> GetByAppointmentIdAsync(Guid appointmentId, CancellationToken cancellationToken);
}