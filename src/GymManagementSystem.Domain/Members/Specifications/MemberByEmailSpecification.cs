using Ardalis.Specification;

namespace GymManagementSystem.Domain.Members.Specifications;

public sealed class MemberByEmailSpecification : Specification<Member>
{
    public MemberByEmailSpecification(string email)
    {
        var normalized = email.Trim().ToLowerInvariant();

        Query
            .Where(m => m.Email.Value == normalized)
            .Include(m => m.Memberships);
    }
}
