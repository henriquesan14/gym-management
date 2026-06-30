using GymManagementSystem.Application.Members;
using GymManagementSystem.Domain.Members;
using GymManagementSystem.Domain.Members.Specifications;

namespace GymManagementSystem.Infra.Data.Repositories;

public sealed class MemberRepository : Repository<Member, MemberId>, IMemberRepository
{
    public MemberRepository(GymManagementDbContext context) : base(context)
    {
    }

    public Task<Member?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        => FirstOrDefaultAsync(new MemberByEmailSpecification(email), cancellationToken);

    public Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default)
        => AnyAsync(new MemberByEmailSpecification(email), cancellationToken);
}
