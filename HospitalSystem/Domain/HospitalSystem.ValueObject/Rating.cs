using Hospital.Domain.ValueObjects.Base;
using Hospital.Domain.ValueObjects.Exceptions;

namespace Hospital.Domain.ValueObjects;

public class Rating(int value) : ValueObject<int>(new RatingValidator(), value);

public class RatingValidator : IValidator<int>
{
    public void Validate(int value)
    {
        if (value < 1 || value > 5)
            throw new InvalidRatingException(value);
    }
}