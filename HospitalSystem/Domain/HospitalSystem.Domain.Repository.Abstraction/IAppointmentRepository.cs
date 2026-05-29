using Hospital.Domain.Entities;

namespace Hospital.Domain.Repositories;

public interface IAppointmentRepository : IRepository<Appointment, Guid>
{
    Task<IReadOnlyList<Appointment>> GetByDoctorAndDateAsync(Guid doctorId, DateTime date, CancellationToken cancellationToken);
    Task<IReadOnlyList<Appointment>> GetByPatientAsync(Guid patientId, CancellationToken cancellationToken);
}