using GymManagementSystem.Application.Users;
using GymManagementSystem.Domain.Users;
using GymManagementSystem.Domain.Users.Specifications;

namespace GymManagementSystem.Infra.Data.Repositories;

public sealed class UserRepository : Repository<User, UserId>, IUserRepository
{
    public UserRepository(GymManagementDbContext context) : base(context)
    {
    }

    public Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default)
        => AnyAsync(new UserByEmailSpecification(email), cancellationToken);

    public Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        => FirstOrDefaultAsync(new UserByEmailSpecification(email), cancellationToken);
}
