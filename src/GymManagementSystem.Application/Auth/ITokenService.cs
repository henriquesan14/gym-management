using GymManagementSystem.Domain.Users;

namespace GymManagementSystem.Application.Auth;

public interface ITokenService
{
    TokenResponse GenerateAccessToken(User user);
}
