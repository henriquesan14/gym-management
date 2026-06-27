using Ardalis.Specification;
using GymManagementSystem.Domain.ValueObjects;

namespace GymManagementSystem.Domain.Members.Specifications;

public sealed class MemberByEmailSpecification : Specification<Member>
{
    public MemberByEmailSpecification(string email)
    {
        var normalized = email.Trim().ToLowerInvariant();

        Query
            .Where(m => m.Email == Email.Of(normalized))
            .Include(m => m.Memberships);
    }
}
