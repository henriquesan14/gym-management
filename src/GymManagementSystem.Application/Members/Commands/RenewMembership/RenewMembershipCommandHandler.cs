using GymManagementSystem.Application.Shared.Contracts;
using GymManagementSystem.Domain.Members.Specifications;
using GymManagementSystem.Shared.Common.CRQS;
using GymManagementSystem.Shared.Common.ResultPattern;

namespace GymManagementSystem.Application.Members.Commands.RenewMembership;

public class RenewMembershipCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<RenewMembershipCommand, Result>
{
    public async Task<Result> Handle(RenewMembershipCommand request, CancellationToken cancellationToken)
    {
        var member = await unitOfWork.Members.SingleOrDefaultAsync(new MemberByIdWithMembershipsSpecification(request.MemberId));

        if (member is null)
            return MemberErrors.NotFound(request.MemberId);

        var membership = member.GetActiveMembership();

        if (membership is null)
            return MemberErrors.WithouActiveMembership();

        membership.Renew(request.Months);

        await unitOfWork.CompleteAsync();

        return Result.Success();
    }
}
