using Hospital.Domain.Entities;
using Hospital.Domain.ValueObjects;
using Hospital.Domain.Enums;

namespace HospitalSystem.DomainApp;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hospital Management System - Domain Layer Demonstration");
        Console.WriteLine();

        Console.WriteLine("[1] Value Objects Creation");

        var patientName = new FullName("Alexey Dmitrievich Kozlov");
        var patientEmail = new Email("alexey.kozlov@mail.com");
        var patientPhone = new PhoneNumber("+7 (999) 123-45-67");
        var patientBirthDate = new DateOnly(1985, 5, 15);
        var patientInsurance = "CHI-1234567890";
        var patientAllergies = "Penicillin";

        var doctorName = new FullName("Elena Vladimirovna Ivanova");
        var doctorEmail = new Email("e.ivanova@hospital.com");
        var doctorPhone = new PhoneNumber("+7 (999) 234-56-78");
        var specialization = new Specialization("Cardiology");
        var cabinetNumber = new CabinetNumber(42);
        var consultationPrice = new Money(2500.00m);

        Console.WriteLine($"Patient: {patientName}, {patientEmail}");
        Console.WriteLine($"Doctor: {doctorName}, {specialization}, Cabinet {cabinetNumber}, Price {consultationPrice}");
        Console.WriteLine();

        Console.WriteLine("[2] Entities Creation");

        var patient = new Patient(
            patientName,
            patientEmail,
            patientPhone,
            patientBirthDate,
            patientInsurance,
            patientAllergies
        );

        var doctor = new Doctor(
            doctorName,
            doctorEmail,
            doctorPhone,
            specialization,
            cabinetNumber,
            consultationPrice,
            "Experienced cardiologist, 15 years of practice"
        );

        Console.WriteLine($"Patient ID: {patient.Id}");
        Console.WriteLine($"Doctor ID: {doctor.Id}");
        Console.WriteLine();

        Console.WriteLine("[3] Doctor Schedule");

        doctor.AddSchedule(DayOfWeek.Monday, new TimeOnly(9, 0), new TimeOnly(13, 0));
        doctor.AddSchedule(DayOfWeek.Tuesday, new TimeOnly(14, 0), new TimeOnly(18, 0));
        doctor.AddSchedule(DayOfWeek.Wednesday, new TimeOnly(9, 0), new TimeOnly(13, 0));

        foreach (var schedule in doctor.Schedules)
        {
            Console.WriteLine($"Schedule: {schedule.Weekday}, {schedule.StartTime} - {schedule.EndTime}, Active: {schedule.IsActive}");
        }
        Console.WriteLine();

        Console.WriteLine("[4] Book Appointment");

        var appointmentDateTime = new DateTime(2026, 5, 4, 10, 0, 0);
        var appointment = patient.BookAppointment(doctor, appointmentDateTime, consultationPrice);

        Console.WriteLine($"Appointment ID: {appointment.Id}");
        Console.WriteLine($"Patient: {patient.Name}");
        Console.WriteLine($"Doctor: {doctor.Name}");
        Console.WriteLine($"DateTime: {appointmentDateTime:yyyy-MM-dd HH:mm}");
        Console.WriteLine($"Price: {appointment.Price}");
        Console.WriteLine($"Status: {appointment.Status}");
        Console.WriteLine();

        Console.WriteLine("[5] Payment Processing");

        var payment = patient.AddPayment(appointment.Id, consultationPrice, "TXN-20260415-001");

        Console.WriteLine($"Payment ID: {payment.Id}");
        Console.WriteLine($"Amount: {payment.Amount}");
        Console.WriteLine($"Status: {payment.Status}");
        Console.WriteLine($"Transaction ID: {payment.TransactionId}");
        Console.WriteLine();

        Console.WriteLine("[6] Medical Record Creation");

        var medicalRecord = patient.AddMedicalRecord(
            appointment.Id,
            "Chest pain, shortness of breath during physical activity",
            "Angina pectoris",
            "Nitroglycerin 0.5 mg as needed for attacks",
            "Further examination recommended"
        );

        Console.WriteLine($"Medical Record ID: {medicalRecord.Id}");
        Console.WriteLine($"Complaints: {medicalRecord.Complaints}");
        Console.WriteLine($"Diagnosis: {medicalRecord.Diagnosis}");
        Console.WriteLine();

        Console.WriteLine("[7] Procedure Prescription");

        var ecg = new Procedure("ECG", new Money(1500.00m), 30, "Electrocardiogram");
        var bloodTest = new Procedure("Blood Test", new Money(800.00m), 15, "Complete blood count");

        medicalRecord.AddProcedure(ecg);
        medicalRecord.AddProcedure(bloodTest, "Fasting required");

        foreach (var proc in medicalRecord.PrescribedProcedures)
        {
            Console.WriteLine($"Procedure: {proc.Procedure.Name}, Price: {proc.Procedure.Price}, Notes: {proc.Notes ?? "None"}");
        }
        Console.WriteLine();

        Console.WriteLine("[8] Complete Appointment");

        var completed = patient.CompleteAppointment(appointment.Id);
        Console.WriteLine($"Appointment Status: {appointment.Status}");
        Console.WriteLine($"Completed At: {appointment.CompletedAt}");
        Console.WriteLine();

        Console.WriteLine("[9] Submit Review");

        var review = doctor.AddReview(patient, 5, "Excellent doctor, very professional and attentive.");
        review.Approve();

        Console.WriteLine($"Review ID: {review.Id}");
        Console.WriteLine($"Rating: {review.Rating}/5");
        Console.WriteLine($"Comment: {review.Comment}");
        Console.WriteLine($"Approved: {review.IsApproved}");
        Console.WriteLine();

        Console.WriteLine("[10] Template Creation and Rendering");

        var template = doctor.AddTemplate("Standard Cardiology Conclusion",
            "Patient: {{PatientName}}\nDiagnosis: {{Diagnosis}}\nRecommendations: {{Recommendations}}\nDate: {{Date}}");

        var replacements = new Dictionary<string, string>
        {
            { "PatientName", patient.Name.ToString() },
            { "Diagnosis", medicalRecord.Diagnosis },
            { "Recommendations", "Continue nitroglycerin as needed, avoid stress, follow-up in 1 month" },
            { "Date", DateTime.Now.ToString("yyyy-MM-dd") }
        };

        var rendered = template.Render(replacements);
        Console.WriteLine($"Template: {template.Name}");
        Console.WriteLine(rendered);
        Console.WriteLine();

        Console.WriteLine("[11] Validation Tests (Expected Exceptions)");

        try
        {
            patient.BookAppointment(doctor, appointmentDateTime, consultationPrice);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Duplicate booking failed as expected: {ex.Message}");
        }

        try
        {
            var pastDate = DateTime.Now.AddDays(-1);
            patient.BookAppointment(doctor, pastDate, consultationPrice);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Past booking failed as expected: {ex.Message}");
        }

        try
        {
            doctor.AddSchedule(DayOfWeek.Monday, new TimeOnly(14, 0), new TimeOnly(13, 0));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Invalid schedule failed as expected: {ex.Message}");
        }

        try
        {
            var invalidEmail = new Email("invalid-email");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Invalid email failed as expected: {ex.Message}");
        }

        try
        {
            var invalidReview = doctor.AddReview(patient, 6, "Invalid rating test");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Invalid rating failed as expected: {ex.Message}");
        }

        Console.WriteLine();

        Console.WriteLine("[12] Final Statistics");

        Console.WriteLine($"Doctor Statistics:");
        Console.WriteLine($"  Appointments: {doctor.Appointments.Count}");
        Console.WriteLine($"  Reviews received: {doctor.Reviews.Count}");
        Console.WriteLine($"  Average rating: {doctor.AverageRating:F1}/5");
        Console.WriteLine();

        Console.WriteLine($"Patient Statistics:");
        Console.WriteLine($"  Appointments: {patient.Appointments.Count}");
        Console.WriteLine($"  Active appointments: {patient.HasActiveAppointments}");
        Console.WriteLine($"  Total spent: {patient.TotalSpent:F2} RUB");
        Console.WriteLine();

        Console.WriteLine("Domain layer demonstration completed successfully.");
    }
}