using GymManagementSystem.Application.Shared.Contracts;
using GymManagementSystem.Domain.Users;

namespace GymManagementSystem.Application.Users;

public interface IUserRepository : IRepository<User, UserId> {
    Task<bool> EmailExistsAsync(
        string email,
        CancellationToken cancellationToken = default);

    Task<User?> GetByEmailAsync(
        string email,
        CancellationToken cancellationToken = default);
}

