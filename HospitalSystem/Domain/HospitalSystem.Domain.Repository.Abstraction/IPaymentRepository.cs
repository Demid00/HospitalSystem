// IPaymentRepository.cs
using Hospital.Domain.Repositories.Abstractions.Base;
using Hospital.Domain.Entities;

namespace Hospital.Domain.Repositories.Abstractions;

public interface IPaymentRepository : IRepository<Payment, Guid>
{
    Task<Payment?> GetByAppointmentIdAsync(Guid appointmentId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Payment>> GetByPatientIdAsync(Guid patientId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Payment>> GetPendingPaymentsAsync(CancellationToken cancellationToken = default);
}