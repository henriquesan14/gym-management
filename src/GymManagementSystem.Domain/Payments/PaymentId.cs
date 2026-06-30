using GymManagementSystem.Domain.Exceptions;

namespace GymManagementSystem.Domain.Payments;

public sealed record PaymentId(Guid Value)
{
    public static PaymentId Of(Guid value)
    {
        if (value == Guid.Empty)
            throw new DomainException("PaymentId cannot be empty.");

        return new(value);
    }

    public static PaymentId New() =>
        new(Guid.NewGuid());
}