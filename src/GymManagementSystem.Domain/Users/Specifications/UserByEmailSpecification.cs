using Ardalis.Specification;
using GymManagementSystem.Domain.ValueObjects;

namespace GymManagementSystem.Domain.Users.Specifications;

public class UserByEmailSpecification : SingleResultSpecification<User>
{
    public UserByEmailSpecification(string email)
    {
        var normalized = email.Trim().ToLowerInvariant();

        Query
            .Where(m => m.Email == Email.Of(normalized));
    }
}
