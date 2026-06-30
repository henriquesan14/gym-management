using GymManagementSystem.Application.Shared.Contracts;
using GymManagementSystem.Application.Users;
using GymManagementSystem.Domain.Users;
using GymManagementSystem.Domain.Users.Contracts;
using GymManagementSystem.Domain.Users.Specifications;
using GymManagementSystem.Shared.Common.CRQS;
using GymManagementSystem.Shared.Common.ResultPattern;

namespace GymManagementSystem.Application.Auth.Commands.RenewAccessToken;

public sealed class RenewAccessTokenCommandHandler(IUnitOfWork unitOfWork, IUserContext userContext, ITokenService tokenService) : ICommandHandler<RenewAccessTokenCommand, ResultT<AuthResponse>>
{
    public async Task<ResultT<AuthResponse>> Handle(RenewAccessTokenCommand request, CancellationToken ct)
    {
        var refreshToken = userContext.RefreshToken;
        var existingToken = await unitOfWork.RefreshTokens
            .SingleOrDefaultAsync(new GetRefreshTokenByTokenSpecification(refreshToken!), ct);

        if (existingToken is null || existingToken.IsExpired || existingToken.IsRevoked)
        {
            return AuthErrors.SessionExpired();
        }

        var user = await unitOfWork.Users.GetByIdAsync(existingToken.UserId, ct);
        if (user is null)
        {
            return UserErrors.NotFound(existingToken.UserId.Value);
        }

        var authToken = tokenService.GenerateAccessToken(user);
        var newRefreshToken = new RefreshToken(RefreshTokenId.New(), authToken.RefreshToken, user.Id, userContext.IpAddress!, DateTime.Now.AddDays(7));

        existingToken.Revoke(userContext.IpAddress!);
        existingToken.SetReplacedByToken(newRefreshToken.Token);


        await unitOfWork.RefreshTokens.AddAsync(newRefreshToken, ct);
        await unitOfWork.CompleteAsync(ct);

        userContext.SetCookieTokens(authToken.AccessToken, authToken.RefreshToken);

        var authResponse = new AuthResponse(user.Id.Value, user.Name, user.Role);
        return authResponse;
    }
}
