using GymManagementSystem.Domain.Exceptions;

namespace GymManagementSystem.Domain.Users;

public sealed record RefreshTokenId(Guid Value)
{
    public static RefreshTokenId Of(Guid value)
    {
        if (value == Guid.Empty)
            throw new DomainException("RefreshTokenId cannot be empty.");

        return new(value);
    }

    public static RefreshTokenId New() =>
        new(Guid.NewGuid());
}
