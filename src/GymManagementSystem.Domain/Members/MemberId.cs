using GymManagementSystem.Domain.Exceptions;

namespace GymManagementSystem.Domain.Members;

public sealed record MemberId(Guid Value)
{
    public static MemberId Of(Guid value)
    {
        if (value == Guid.Empty)
            throw new DomainException("MemberId cannot be empty.");

        return new(value);
    }

    public static MemberId New() =>
        new(Guid.NewGuid());
}
