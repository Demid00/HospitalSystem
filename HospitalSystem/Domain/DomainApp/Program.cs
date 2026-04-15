// DomainApp/Program.cs
using Hospital.Domain;
using Hospital.Domain.Entities;
using Hospital.Domain.Enums;
using Hospital.ValueObjects;

namespace DomainApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=".PadRight(80, '='));
            Console.WriteLine("HOSPITAL MANAGEMENT SYSTEM - DOMAIN LAYER TEST");
            Console.WriteLine("=".PadRight(80, '='));
            Console.WriteLine();

            try
            {
                // ============================================================
                // 1. CREATE USERS
                // ============================================================
                Console.WriteLine("[1] CREATING USERS");
                Console.WriteLine("-".PadRight(80, '-'));

                var adminUser = new User(
                    new Email("admin@hospital.com"),
                    new FullName("Ivan Petrovich Smirnov"),
                    new PhoneNumber("+7 (999) 123-45-67"),
                    UserRole.Admin
                );

                var doctorUser = new User(
                    new Email("doctor@hospital.com"),
                    new FullName("Elena Vladimirovna Ivanova"),
                    new PhoneNumber("+7 (999) 234-56-78"),
                    UserRole.Doctor
                );

                var patientUser = new User(
                    new Email("patient@mail.com"),
                    new FullName("Alexey Dmitrievich Kozlov"),
                    new PhoneNumber("+7 (999) 345-67-89"),
                    UserRole.Patient
                );

                Console.WriteLine($"User created (Admin):  {adminUser.FullName.Value} <{adminUser.Email.Value}>");
                Console.WriteLine($"User created (Doctor): {doctorUser.FullName.Value} <{doctorUser.Email.Value}>");
                Console.WriteLine($"User created (Patient):{patientUser.FullName.Value} <{patientUser.Email.Value}>");
                Console.WriteLine();

                // ============================================================
                // 2. CREATE DOCTOR AND PATIENT
                // ============================================================
                Console.WriteLine("[2] CREATING DOCTOR AND PATIENT ENTITIES");
                Console.WriteLine("-".PadRight(80, '-'));

                var doctor = new Doctor(
                    doctorUser,
                    new Specialization("Cardiology"),
                    new CabinetNumber(42),
                    new Money(2500.00m),
                    "Experienced cardiologist, 15 years of practice"
                );

                var patient = new Patient(
                    patientUser,
                    new DateOnly(1985, 5, 15),
                    "CHI-1234567890",
                    "Penicillin"
                );

                Console.WriteLine($"Doctor: {doctor.Specialization.Value}, Cabinet {doctor.CabinetNumber.Value}");
                Console.WriteLine($"        Consultation price: {doctor.ConsultationPrice.Value:F2} {doctor.ConsultationPrice.Currency}");
                Console.WriteLine($"Patient: DOB {patient.BirthDate:yyyy-MM-dd}, Allergies: {patient.Allergies ?? "None"}");
                Console.WriteLine();

                // ============================================================
                // 3. CREATE SCHEDULE
                // ============================================================
                Console.WriteLine("[3] CREATING DOCTOR SCHEDULE");
                Console.WriteLine("-".PadRight(80, '-'));

                var scheduleMonday = new Schedule(doctor, DayOfWeek.Monday, new TimeOnly(9, 0), new TimeOnly(13, 0));
                var scheduleTuesday = new Schedule(doctor, DayOfWeek.Tuesday, new TimeOnly(14, 0), new TimeOnly(18, 0));
                var scheduleWednesday = new Schedule(doctor, DayOfWeek.Wednesday, new TimeOnly(9, 0), new TimeOnly(13, 0));

                doctor.AddSchedule(scheduleMonday);
                doctor.AddSchedule(scheduleTuesday);
                doctor.AddSchedule(scheduleWednesday);

                Console.WriteLine("Working hours:");
                foreach (var s in doctor.Schedules.Where(s => s.IsActive))
                {
                    Console.WriteLine($"  {s.Weekday}: {s.StartTime:hh\\:mm} - {s.EndTime:hh\\:mm}");
                }
                Console.WriteLine();

                // ============================================================
                // 4. BOOK APPOINTMENT
                // ============================================================
                Console.WriteLine("[4] BOOKING APPOINTMENT");
                Console.WriteLine("-".PadRight(80, '-'));

                var appointmentDateTime = new DateTime(2026, 4, 20, 10, 0, 0);
                var appointment = patient.BookAppointment(doctor, appointmentDateTime, doctor.ConsultationPrice);

                Console.WriteLine($"Appointment ID: {appointment.Id}");
                Console.WriteLine($"Patient: {patient.User.FullName.Value}");
                Console.WriteLine($"Doctor: {doctor.User.FullName.Value} ({doctor.Specialization.Value})");
                Console.WriteLine($"DateTime: {appointmentDateTime:yyyy-MM-dd HH:mm}");
                Console.WriteLine($"Price: {appointment.Price.Value:F2} {appointment.Price.Currency}");
                Console.WriteLine($"Status: {appointment.Status}");
                Console.WriteLine();

                // ============================================================
                // 5. PROCESS PAYMENT
                // ============================================================
                Console.WriteLine("[5] PROCESSING PAYMENT");
                Console.WriteLine("-".PadRight(80, '-'));

                var payment = new Payment(appointment, appointment.Price);
                appointment.AddPayment(payment);
                payment.MarkAsPaid("TXN-20260415-001");

                Console.WriteLine($"Payment ID: {payment.Id}");
                Console.WriteLine($"Amount: {payment.Amount.Value:F2} {payment.Amount.Currency}");
                Console.WriteLine($"Status: {payment.Status}");
                Console.WriteLine($"Transaction ID: {payment.TransactionId}");
                Console.WriteLine();

                // ============================================================
                // 6. CREATE MEDICAL RECORD
                // ============================================================
                Console.WriteLine("[6] CREATING MEDICAL RECORD");
                Console.WriteLine("-".PadRight(80, '-'));

                var medicalRecord = new MedicalRecord(
                    appointment,
                    "Chest pain, shortness of breath during physical activity",
                    "Angina pectoris",
                    "Nitroglycerin 0.5 mg as needed for attacks",
                    "Further examination recommended"
                );
                appointment.AddMedicalRecord(medicalRecord);

                Console.WriteLine($"Medical Record ID: {medicalRecord.Id}");
                Console.WriteLine($"Complaints: {medicalRecord.Complaints}");
                Console.WriteLine($"Diagnosis: {medicalRecord.Diagnosis}");
                Console.WriteLine($"Treatment: {medicalRecord.Treatment}");
                Console.WriteLine();

                // ============================================================
                // 7. ADD PROCEDURES
                // ============================================================
                Console.WriteLine("[7] PRESCRIBING PROCEDURES");
                Console.WriteLine("-".PadRight(80, '-'));

                var ecg = new Procedure("ECG", new Money(1500.00m), 30, "Electrocardiogram");
                var bloodTest = new Procedure("Blood Test", new Money(800.00m), 15, "Complete blood count");

                medicalRecord.AddPrescribedProcedure(ecg);
                medicalRecord.AddPrescribedProcedure(bloodTest, "Fasting required");

                Console.WriteLine("Prescribed procedures:");
                foreach (var proc in medicalRecord.PrescribedProcedures)
                {
                    Console.WriteLine($"  - {proc.Procedure.Name}: {proc.Procedure.Price.Value:F2} {proc.Procedure.Price.Currency}");
                    if (!string.IsNullOrEmpty(proc.Notes))
                        Console.WriteLine($"    Notes: {proc.Notes}");
                }
                Console.WriteLine();

                // ============================================================
                // 8. COMPLETE APPOINTMENT
                // ============================================================
                Console.WriteLine("[8] COMPLETING APPOINTMENT");
                Console.WriteLine("-".PadRight(80, '-'));

                appointment.Complete();

                Console.WriteLine($"Appointment ID: {appointment.Id}");
                Console.WriteLine($"Status: {appointment.Status}");
                Console.WriteLine($"Completed at: {appointment.CompletedAt:yyyy-MM-dd HH:mm:ss}");
                Console.WriteLine();

                // ============================================================
                // 9. SUBMIT REVIEW
                // ============================================================
                Console.WriteLine("[9] SUBMITTING REVIEW");
                Console.WriteLine("-".PadRight(80, '-'));

                var review = patient.WriteReview(doctor, 5, "Excellent doctor, very professional and attentive.");
                review.Approve();

                Console.WriteLine($"Review ID: {review.Id}");
                Console.WriteLine($"Rating: {review.Rating}/5");
                Console.WriteLine($"Comment: {review.Comment}");
                Console.WriteLine($"Approved: {review.IsApproved}");
                Console.WriteLine();

                // ============================================================
                // 10. VALIDATION TESTS (EXPECTED ERRORS)
                // ============================================================
                Console.WriteLine("[10] VALIDATION TESTS (EXPECTED EXCEPTIONS)");
                Console.WriteLine("-".PadRight(80, '-'));

                // Test 1: Duplicate appointment
                try
                {
                    var duplicateAppointment = patient.BookAppointment(doctor, appointmentDateTime, doctor.ConsultationPrice);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"  [FAIL] Duplicate booking: {ex.Message}");
                }

                // Test 2: Cancel completed appointment
                try
                {
                    appointment.Cancel();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"  [FAIL] Cancel completed: {ex.Message}");
                }

                // Test 3: Invalid rating
                try
                {
                    var invalidReview = patient.WriteReview(doctor, 6, "Invalid rating test");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"  [FAIL] Invalid rating: {ex.Message}");
                }

                // Test 4: Invalid email
                try
                {
                    var invalidEmail = new Email("invalid-email");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"  [FAIL] Invalid email: {ex.Message}");
                }

                // Test 5: Invalid schedule time
                try
                {
                    var invalidSchedule = new Schedule(doctor, DayOfWeek.Monday, new TimeOnly(14, 0), new TimeOnly(13, 0));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"  [FAIL] Invalid schedule: {ex.Message}");
                }

                Console.WriteLine();

                // ============================================================
                // 11. FINAL STATISTICS
                // ============================================================
                Console.WriteLine("[11] FINAL STATISTICS");
                Console.WriteLine("-".PadRight(80, '-'));

                Console.WriteLine("DOCTOR STATISTICS");
                Console.WriteLine($"  Appointments conducted: {doctor.Appointments.Count}");
                Console.WriteLine($"  Medical records created: {doctor.MedicalRecords.Count}");
                Console.WriteLine($"  Reviews received: {doctor.Reviews.Count}");
                Console.WriteLine($"  Average rating: {(doctor.Reviews.Any() ? doctor.Reviews.Average(r => r.Rating).ToString("F1") : "N/A")}/5");
                Console.WriteLine();

                Console.WriteLine("PATIENT STATISTICS");
                Console.WriteLine($"  Appointments booked: {patient.Appointments.Count}");
                Console.WriteLine($"  Reviews submitted: {patient.Reviews.Count}");
                Console.WriteLine();

                Console.WriteLine("FINANCIAL SUMMARY");
                Console.WriteLine($"  Appointment price: {appointment.Price.Value:F2} {appointment.Price.Currency}");
                Console.WriteLine($"  Payment status: {payment.Status}");
                Console.WriteLine();

                // ============================================================
                // COMPLETION
                // ============================================================
                Console.WriteLine("=".PadRight(80, '='));
                Console.WriteLine("DOMAIN LAYER TEST COMPLETED SUCCESSFULLY");
                Console.WriteLine("=".PadRight(80, '='));
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine("ERROR".PadRight(80, '='));
                Console.WriteLine($"Exception: {ex.GetType().Name}");
                Console.WriteLine($"Message: {ex.Message}");
                Console.WriteLine("=".PadRight(80, '='));
                Console.WriteLine();
                Console.WriteLine("Stack trace:");
                Console.WriteLine(ex.StackTrace);
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}