using GymManagementSystem.Domain.Abstractions;
using GymManagementSystem.Domain.Members.Contracts;

namespace GymManagementSystem.Domain.Members.Rules;

public sealed class EmailMustNotBeUsed(string email, IMemberRuleCheck check) : IBusinessRule
{
    public string Message => "Email member is used";

    public bool IsBroken()
    {
        return check.MemberEmailExists(email);
    }
}
