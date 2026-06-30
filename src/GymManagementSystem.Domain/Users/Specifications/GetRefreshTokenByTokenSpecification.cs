using Ardalis.Specification;

namespace GymManagementSystem.Domain.Users.Specifications;

public sealed class GetRefreshTokenByTokenSpecification : SingleResultSpecification<RefreshToken>
{
    public GetRefreshTokenByTokenSpecification(string refreshToken)
    {
        Query
            .Where(rt => rt.Token == refreshToken);
    }
}
