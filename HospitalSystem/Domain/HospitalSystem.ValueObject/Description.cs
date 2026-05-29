using Hospital.Domain.ValueObjects.Base;

namespace Hospital.Domain.ValueObjects;

public class Description(string? value) : ValueObject<string?>(new DescriptionValidator(), value);

public class DescriptionValidator : IValidator<string?>
{
    public void Validate(string? value) { /* необязательное поле – валидация не требуется */ }
}