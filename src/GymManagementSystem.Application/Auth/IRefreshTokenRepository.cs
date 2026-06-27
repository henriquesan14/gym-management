using GymManagementSystem.Application.Shared.Contracts;
using GymManagementSystem.Domain.Users;

namespace GymManagementSystem.Application.Auth;

public interface IRefreshTokenRepository : IRepository<RefreshToken, RefreshTokenId>;
