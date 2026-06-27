using GymManagementSystem.Application.Auth;
using GymManagementSystem.Domain.Users;

namespace GymManagementSystem.Infra.Data.Repositories;

public class RefreshTokenRepository : Repository<RefreshToken, RefreshTokenId>, IRefreshTokenRepository
{
    public RefreshTokenRepository(GymManagementDbContext db) : base(db)
    {
    }
}
