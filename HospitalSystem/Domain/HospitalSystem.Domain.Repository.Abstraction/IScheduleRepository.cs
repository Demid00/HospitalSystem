// IScheduleRepository.cs
using Hospital.Domain.Repositories.Abstractions.Base;
using Hospital.Domain.Entities;

namespace Hospital.Domain.Repositories.Abstractions;

public interface IScheduleRepository : IRepository<Schedule, Guid>
{
    Task<IReadOnlyList<Schedule>> GetByDoctorIdAsync(Guid doctorId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Schedule>> GetActiveByDoctorIdAsync(Guid doctorId, CancellationToken cancellationToken = default);
    Task<Schedule?> GetByDoctorAndWeekdayAsync(Guid doctorId, DayOfWeek weekday, CancellationToken cancellationToken = default);
}