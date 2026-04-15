// Exceptions/ReviewExceptions.cs
namespace Hospital.Domain.Exceptions;

public class InvalidRatingException : DomainException
{
    public int Rating { get; }

    public InvalidRatingException(int rating)
        : base($"Rating {rating} is invalid. Rating must be between 1 and 5.")
    {
        Rating = rating;
    }
}

public class ReviewAlreadyExistsException : DomainException
{
    public Guid PatientId { get; }
    public Guid DoctorId { get; }

    public ReviewAlreadyExistsException(Guid patientId, Guid doctorId)
        : base($"Review already exists for patient {patientId} and doctor {doctorId}.")
    {
        PatientId = patientId;
        DoctorId = doctorId;
    }
}