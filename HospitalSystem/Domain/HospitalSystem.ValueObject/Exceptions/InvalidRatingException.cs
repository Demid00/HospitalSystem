namespace Hospital.Domain.ValueObjects.Exceptions;

public class InvalidRatingException(int rating)
    : ValueObjectException($"Rating {rating} is out of range (1-5).");