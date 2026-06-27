using GymManagementSystem.Shared.Common.ResultPattern;

namespace GymManagementSystem.Application.Members;
public static class MemberErrors
{
    public static Error NotFound(Guid id) =>
        Error.NotFound("Member.NotFound", $"Member with Id: {id} not found");

    public static Error Conflict(string email) =>
        Error.Conflict("Member.Conflict", $"Member with Email: {email} already exists");

    public static Error WithouActiveMembership() =>
        Error.Conflict("Member.Conflict", $"Member doesn't have an Active Membership");

    public static Error CreateFailure =>
        Error.Failure("Member.CreateFailure", $"Something went wrong in creating Member");

    public static Error UpdateFailure =>
        Error.Failure("Member.UpdateFailure", $"Something went wrong in updating Member");

    public static Error DeleteFailure =>
        Error.Failure("Member.DeleteFailure", $"Something went wrong in deleting Member");
}
