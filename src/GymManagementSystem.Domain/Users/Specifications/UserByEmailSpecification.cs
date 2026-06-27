using Ardalis.Specification;

namespace GymManagementSystem.Domain.Users.Specifications;

public class UserByEmailSpecification : SingleResultSpecification<User>
{
    public UserByEmailSpecification(string email)
    {
        var normalized = email.Trim().ToLowerInvariant();

        Query
            .Where(m => m.Email.Value == normalized);
    }
}
