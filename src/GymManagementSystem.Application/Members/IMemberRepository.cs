using GymManagementSystem.Application.Shared.Contracts;
using GymManagementSystem.Domain.Members;

namespace GymManagementSystem.Application.Members;

public interface IMemberRepository : IRepository<Member, MemberId>
{
    Task<Member?> GetByEmailAsync(
        string email,
        CancellationToken cancellationToken = default);

    Task<bool> EmailExistsAsync(
        string email,
        CancellationToken cancellationToken = default);
}
