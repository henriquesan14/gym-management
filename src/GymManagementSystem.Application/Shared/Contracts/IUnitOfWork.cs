using GymManagementSystem.Application.Members;
using GymManagementSystem.Application.Users;

namespace GymManagementSystem.Application.Shared.Contracts;

public interface IUnitOfWork
{
    IMemberRepository Members { get; }
    IUserRepository Users { get; }
    Task<int> CompleteAsync();
    Task BeginTransaction();
    Task CommitAsync();
}
