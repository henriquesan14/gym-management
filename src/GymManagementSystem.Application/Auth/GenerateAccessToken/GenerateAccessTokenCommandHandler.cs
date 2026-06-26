using GymManagementSystem.Application.Shared.Contracts;
using GymManagementSystem.Domain.Users.Contracts;
using GymManagementSystem.Shared.Common.CRQS;
using GymManagementSystem.Shared.Common.ResultPattern;

namespace GymManagementSystem.Application.Auth.GenerateAccessToken;

public class GenerateAccessTokenCommandHandler(IUnitOfWork unitOfWork, ITokenService tokenService,
    IPasswordCheck passwordCheck, IUserContext userContext) : ICommandHandler<GenerateAccessTokenCommand, ResultT<AuthResponse>>
{
    public async Task<ResultT<AuthResponse>> Handle(GenerateAccessTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.Users.GetByEmailAsync(request.Email);
        if (user == null) return AuthErrors.Unauthorized();

        var canLogin = user.CanUserLogin(request.Password, passwordCheck);
        if (!canLogin) return AuthErrors.Unauthorized();

        var tokenResponse = tokenService.GenerateAccessToken(user);

        userContext.SetCookieTokens(tokenResponse.AccessToken, tokenResponse.AccessToken);

        var authResponse = new AuthResponse(user.Id.Value, user.Name, user.Role);

        return authResponse;
    }
}
