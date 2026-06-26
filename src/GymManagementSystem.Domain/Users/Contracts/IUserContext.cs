namespace GymManagementSystem.Domain.Users.Contracts;

public interface IUserContext
{
    Guid? UserId { get; }
    string? UserName { get; }
    Guid? TenantId { get; }
    //UserRoleEnum? Role { get; }
    bool IsAdminSystem { get; }
    string? IpAddress { get; }
    string? RefreshToken { get; }
    void SetCookieTokens(string accessToken, string refreshToken);
    void RemoveCookiesToken();
}
