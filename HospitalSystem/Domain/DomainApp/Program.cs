using Hospital.Domain.Entities;
using Hospital.Domain.Enums;
using Hospital.Domain.ValueObjects;

namespace HospitalSystem.DomainApp;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Hospital Domain Layer Demo (Clean Architecture + DDD) ===\n");

        // 1. Создание Value Objects
        var patientName = new FullName("Alexey Dmitrievich Kozlov");
        var patientEmail = new Email("alexey.kozlov@mail.com");
        var patientPhone = new PhoneNumber("+7 (999) 123-45-67");
        var patientBirth = new DateOnly(1985, 5, 15);
        var insurance = new InsurancePolicy("CHI-1234567890");
        var allergies = "Penicillin";

        var doctorName = new FullName("Elena Vladimirovna Ivanova");
        var doctorEmail = new Email("e.ivanova@hospital.com");
        var doctorPhone = new PhoneNumber("+7 (999) 234-56-78");
        var specialization = new Specialization("Cardiology");
        var cabinet = new CabinetNumber(42);
        var description = new Description("Experienced cardiologist, 15 years of practice");

        Console.WriteLine("Value Objects created successfully.");

        // 2. Создание сущностей (агрегатов) – передаём дату создания
        var now = DateTime.UtcNow;
        var patient = new Patient(patientName, patientEmail, patientPhone, patientBirth,
                                  insurance, allergies, now);
        var doctor = new Doctor(doctorName, doctorEmail, doctorPhone, specialization,
                                cabinet, description, now);

        Console.WriteLine($"Patient created: {patient.Id}");
        Console.WriteLine($"Doctor created: {doctor.Id}");

        // 3. Создание Appointment (дата в будущем – через 30 дней от now)
        var futureDate = now.AddDays(30);
        // Приводим время к рабочему часу (10:00), чтобы избежать проверки времени
        var appointmentDate = new DateTime(futureDate.Year, futureDate.Month, futureDate.Day, 10, 0, 0);
        var appointmentDateTime = new AppointmentDateTime(appointmentDate);
        var appointment = new Appointment(patient.Id, doctor.Id, appointmentDateTime, now);

        Console.WriteLine($"Appointment created: {appointment.Id}, Status: {appointment.Status}");

        // 4. Отмена записи (доменное событие)
        var cancelReason = new CancellationReason("Patient changed mind");
        appointment.Cancel(cancelReason, now);
        Console.WriteLine($"Appointment cancelled. Events: {appointment.DomainEvents.Count}");
        foreach (var evt in appointment.DomainEvents)
        {
            Console.WriteLine($"  - {evt.GetType().Name}");
        }

        // 5. Медицинская запись (используем ID отменённого приёма – допустимо для демонстрации)
        var complaints = new Complaints("Chest pain, shortness of breath");
        var diagnosis = new Diagnosis("Angina pectoris");
        var treatment = new Treatment("Nitroglycerin 0.5 mg as needed");
        var conclusion = new Conclusion("Further examination recommended");

        var medicalRecord = new MedicalRecord(appointment.Id, complaints, diagnosis,
                                              treatment, conclusion, now);
        medicalRecord.AddPrescribedProcedure("ECG", "Fasting required", now);
        medicalRecord.AddPrescribedProcedure("Blood Test", null, now);

        Console.WriteLine($"Medical record created. Procedures: {medicalRecord.PrescribedProcedures.Count}");
        foreach (var proc in medicalRecord.PrescribedProcedures)
            Console.WriteLine($"  - {proc.ProcedureName}, Completed: {proc.IsCompleted}");

        // 6. Отзыв
        var rating = new Rating(5);
        var review = new Review(doctor.Id, patient.Id, rating, "Excellent doctor!", now);
        review.Approve();
        Console.WriteLine($"Review created and approved. Events: {review.DomainEvents.Count}");

        // 7. Проверка доменных исключений
        try
        {
            var invalidName = new FullName("A"); // слишком короткое
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Expected validation error: {ex.Message}");
        }

        try
        {
            appointment.Complete(now); // уже отменён -> исключение
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Expected domain error: {ex.Message}");
        }

        // 8. Дополнительная проверка AppointmentDateTime (прошлая дата)
        try
        {
            var pastDateTime = new AppointmentDateTime(DateTime.UtcNow.AddDays(-1));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Expected domain error (past date): {ex.Message}");
        }

        Console.WriteLine("\nDomain layer demo completed.");
    }
}