using Ardalis.Specification;

namespace GymManagementSystem.Domain.Users.Specifications;

public class GetRefreshTokenByTokenSpecification : SingleResultSpecification<RefreshToken>
{
    public GetRefreshTokenByTokenSpecification(string refreshToken)
    {
        Query
            .Where(rt => rt.Token == refreshToken);
    }
}
