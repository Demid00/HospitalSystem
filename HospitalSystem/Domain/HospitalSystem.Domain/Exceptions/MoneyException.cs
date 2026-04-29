namespace Hospital.Domain.Exceptions;

public class InsufficientFundsException(decimal required, decimal available)
    : DomainException($"Insufficient funds. Required: {required}, Available: {available}");

public class InvalidEmailFormatException(string email)
    : DomainException($"Email '{email}' has invalid format.");

public class InvalidPhoneNumberFormatException(string phone)
    : DomainException($"Phone number '{phone}' has invalid format.");

public class MoneyAmountNonPositiveException(decimal amount)
    : DomainException($"Money amount {amount} must be positive.");

public class MoneyAmountHasMoreThanTwoDecimalPlacesException(decimal amount)
    : DomainException($"Money amount {amount} has more than two decimal places.");

public class FullNameShortValueException(string value, int minLength)
    : DomainException($"Full name '{value}' length {value.Length} is less than minimum {minLength}.");

public class FullNameLongValueException(string value, int maxLength)
    : DomainException($"Full name '{value}' length {value.Length} is greater than maximum {maxLength}.");

public class SpecializationShortValueException(string value, int minLength)
    : DomainException($"Specialization '{value}' length {value.Length} is less than minimum {minLength}.");

public class SpecializationLongValueException(string value, int maxLength)
    : DomainException($"Specialization '{value}' length {value.Length} is greater than maximum {maxLength}.");

public class CabinetNumberMinValueException(int value, int minValue)
    : DomainException($"Cabinet number {value} is less than minimum {minValue}.");

public class CabinetNumberMaxValueException(int value, int maxValue)
    : DomainException($"Cabinet number {value} is greater than maximum {maxValue}.");

/// <summary>
/// Exception thrown when a rating value is invalid (not between 1 and 5).
/// </summary>
public class InvalidRatingException : DomainException
{
    public int Rating { get; }

    public InvalidRatingException(int rating)
        : base($"Rating {rating} is invalid. Rating must be between 1 and 5.")
    {
        Rating = rating;
    }
}