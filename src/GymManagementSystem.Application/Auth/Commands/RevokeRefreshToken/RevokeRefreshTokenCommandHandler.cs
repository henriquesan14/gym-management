using GymManagementSystem.Application.Shared.Contracts;
using GymManagementSystem.Domain.Users.Contracts;
using GymManagementSystem.Domain.Users.Specifications;
using GymManagementSystem.Shared.Common.CRQS;
using GymManagementSystem.Shared.Common.ResultPattern;

namespace GymManagementSystem.Application.Auth.Commands.RevokeRefreshToken;

public sealed class RevokeRefreshTokenCommandHandler(IUnitOfWork unitOfWork, IUserContext userContext) : ICommandHandler<RevokeRefreshTokenCommand, Result>
{
    public async Task<Result> Handle(
        RevokeRefreshTokenCommand request,
        CancellationToken ct)
    {
        var refreshToken = userContext.RefreshToken;

        if (string.IsNullOrWhiteSpace(refreshToken))
        {
            userContext.RemoveCookiesToken();
            return Result.Success();
        }

        var token = await unitOfWork.RefreshTokens.SingleOrDefaultAsync(
            new GetRefreshTokenByTokenSpecification(refreshToken), ct);

        if (token is not null && !token.IsRevoked)
        {
            token.Revoke(userContext.IpAddress!);
            await unitOfWork.CompleteAsync(ct);
        }

        userContext.RemoveCookiesToken();

        return Result.Success();
    }
}
