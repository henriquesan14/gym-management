using GymManagementSystem.Domain.Exceptions;

namespace GymManagementSystem.Domain.Members;

public class CheckInId
{
    public Guid Value { get; }

    private CheckInId(Guid value) => Value = value;

    public static CheckInId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty)
        {
            throw new DomainException("CheckInId cannot be empty.");
        }
        return new CheckInId(value);
    }
}
