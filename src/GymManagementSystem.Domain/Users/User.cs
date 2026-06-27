using GymManagementSystem.Domain.Abstractions;
using GymManagementSystem.Domain.Enums;
using GymManagementSystem.Domain.Users.Contracts;
using GymManagementSystem.Domain.ValueObjects;

namespace GymManagementSystem.Domain.Users;

public class User : Aggregate<UserId>, IAuditableEntity
{
    private User()
    {
    }

    public User(string name, Email email, UserRole role, string password, IPasswordHash passwordHash)
    {
        Id = UserId.Of(Guid.NewGuid());
        Name = name;
        Email = email;
        PasswordHash = passwordHash.HashPassword(password);
        Role = role;
    }

    public void Update(string name, Email email, UserRole role)
    {
        Name = name;
        Email = email;
        Role = role;
    }

    private readonly List<RefreshToken> _refreshTokens = [];
    public string Name { get; private set; } = default!;
    public Email Email { get; private set; } = default!;
    public string PasswordHash { get; private set; } = default!;
    public UserRole Role { get; private set; } = default!;

    public IReadOnlyCollection<RefreshToken> RefreshTokens =>
        _refreshTokens.AsReadOnly();

    public bool CanUserLogin(string password, IPasswordCheck checker)
    {
        return checker.Matches(password, PasswordHash);
    }

    public void ChangeRole(UserRole role)
    {
        Role = role;
    }

    public void ChangeName(string name)
    {
        Name = name;
    }

    public void ChangePassword(string oldPassword, string newPassword, IPasswordCheck checker, IPasswordHash hasher)
    {
        PasswordHash = hasher.HashPassword(newPassword);
    }
}
