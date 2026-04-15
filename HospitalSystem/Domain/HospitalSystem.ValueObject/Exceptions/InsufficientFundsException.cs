// Exceptions/InsufficientFundsException.cs
using System;

namespace Hospital.ValueObjects.Exceptions;

public class InsufficientFundsException : Exception
{
    public decimal Required { get; }
    public decimal Available { get; }

    public InsufficientFundsException(decimal required, decimal available)
        : base($"Insufficient funds. Required: {required}, Available: {available}")
    {
        Required = required;
        Available = available;
    }
}