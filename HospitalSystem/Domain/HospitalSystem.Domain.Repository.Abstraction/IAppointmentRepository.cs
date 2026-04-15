// IAppointmentRepository.cs
using Hospital.Domain.Repositories.Abstractions.Base;
using Hospital.Domain.Entities;

namespace Hospital.Domain.Repositories.Abstractions;

public interface IAppointmentRepository : IRepository<Appointment, Guid>
{
    Task<IReadOnlyList<Appointment>> GetByDoctorIdAsync(Guid doctorId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Appointment>> GetByDoctorIdAsync(Guid doctorId, DateTime from, DateTime to, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Appointment>> GetByPatientIdAsync(Guid patientId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Appointment>> GetByPatientIdAsync(Guid patientId, DateTime from, DateTime to, CancellationToken cancellationToken = default);
    Task<bool> IsTimeSlotAvailableAsync(Guid doctorId, DateTime dateTime, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Appointment>> GetUpcomingForDoctorAsync(Guid doctorId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Appointment>> GetUpcomingForPatientAsync(Guid patientId, CancellationToken cancellationToken = default);
}