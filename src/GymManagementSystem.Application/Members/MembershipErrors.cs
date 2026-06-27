using GymManagementSystem.Shared.Common.ResultPattern;

namespace GymManagementSystem.Application.Members;

public static class MembershipErrors
{
    public static Error NotFound(Guid id) =>
        Error.NotFound("Membership.NotFound", $"Membership with Id: {id} not found");
}
