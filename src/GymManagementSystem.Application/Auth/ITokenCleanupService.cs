namespace GymManagementSystem.Application.Auth;

public interface ITokenCleanupService
{
    Task CleanupExpiredAndRevokedTokensAsync();
}
