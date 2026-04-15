// IMedicalRecordRepository.cs
using Hospital.Domain.Repositories.Abstractions.Base;
using Hospital.Domain.Entities;

namespace Hospital.Domain.Repositories.Abstractions;

public interface IMedicalRecordRepository : IRepository<MedicalRecord, Guid>
{
    Task<MedicalRecord?> GetByAppointmentIdAsync(Guid appointmentId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<MedicalRecord>> GetByPatientIdAsync(Guid patientId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<MedicalRecord>> GetByDoctorIdAsync(Guid doctorId, CancellationToken cancellationToken = default);
}