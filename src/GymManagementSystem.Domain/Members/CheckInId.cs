using GymManagementSystem.Domain.Exceptions;

namespace GymManagementSystem.Domain.Members;

public sealed record CheckInId(Guid Value)
{
    public static CheckInId Of(Guid value)
    {
        if (value == Guid.Empty)
            throw new DomainException("CheckInId cannot be empty.");

        return new(value);
    }

    public static CheckInId New() =>
        new(Guid.NewGuid());
}
