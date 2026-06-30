using GymManagementSystem.Application.Shared.Contracts;
using GymManagementSystem.Domain.Users;
using GymManagementSystem.Domain.Users.Contracts;
using GymManagementSystem.Shared.Common.CRQS;
using GymManagementSystem.Shared.Common.ResultPattern;

namespace GymManagementSystem.Application.Auth.Commands.GenerateAccessToken;

public sealed class GenerateAccessTokenCommandHandler(IUnitOfWork unitOfWork, ITokenService tokenService,
    IPasswordCheck passwordCheck, IUserContext userContext) : ICommandHandler<GenerateAccessTokenCommand, ResultT<AuthResponse>>
{
    public async Task<ResultT<AuthResponse>> Handle(GenerateAccessTokenCommand request, CancellationToken ct)
    {
        var user = await unitOfWork.Users.GetByEmailAsync(request.Email, ct);
        if (user == null) return AuthErrors.Unauthorized();

        var canLogin = user.CanUserLogin(request.Password, passwordCheck);
        if (!canLogin) return AuthErrors.Unauthorized();

        var tokenResponse = tokenService.GenerateAccessToken(user);

        var refreshToken = new RefreshToken(
            RefreshTokenId.New(),
            tokenResponse.RefreshToken,
            UserId.Of(user.Id.Value),
            userContext.IpAddress!,
            tokenResponse.RefreshTokenExpiresAt
        );

        await unitOfWork.RefreshTokens.AddAsync(refreshToken, ct);
        await unitOfWork.CompleteAsync(ct);

        userContext.SetCookieTokens(tokenResponse.AccessToken, tokenResponse.RefreshToken);

        var authResponse = new AuthResponse(user.Id.Value, user.Name, user.Role);

        return authResponse;
    }
}
