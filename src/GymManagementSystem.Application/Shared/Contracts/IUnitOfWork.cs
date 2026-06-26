using GymManagementSystem.Application.Members;

namespace GymManagementSystem.Application.Shared.Contracts;

public interface IUnitOfWork
{
    IMemberRepository Members { get; }
    Task<int> CompleteAsync();
    Task BeginTransaction();
    Task CommitAsync();
}
