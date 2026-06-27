using Ardalis.Specification;

namespace GymManagementSystem.Domain.Members.Specifications;

public class MemberByIdWithMembershipsSpecification : SingleResultSpecification<Member>
{
    public MemberByIdWithMembershipsSpecification(Guid Id)
    {
        Query
            .Where(m => m.Id == MemberId.Of(Id))
            .Include(m => m.Memberships);
    }
}
