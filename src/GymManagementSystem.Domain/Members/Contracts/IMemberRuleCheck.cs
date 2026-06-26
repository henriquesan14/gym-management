namespace GymManagementSystem.Domain.Members.Contracts;
public interface IMemberRuleCheck
{
    bool MemberEmailExists(string email);
}

