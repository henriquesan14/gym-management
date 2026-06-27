using GymManagementSystem.Application.Shared.Contracts;
using GymManagementSystem.Domain.Members;
using GymManagementSystem.Domain.Members.Specifications;
using GymManagementSystem.Shared.Common.CRQS;
using GymManagementSystem.Shared.Common.ResultPattern;

namespace GymManagementSystem.Application.Members.Commands.CancelMembership;

public class CancelMembershipCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<CancelMembershipCommand, Result>
{
    public async Task<Result> Handle(CancelMembershipCommand request, CancellationToken cancellationToken)
    {
        var member = await unitOfWork.Members.SingleOrDefaultAsync(new MemberByIdWithMembershipsSpecification(request.MemberId));

        if (member is null)
            return MemberErrors.NotFound(request.MemberId);

        member.CancelMembership(MembershipId.Of(request.MembershipId));

        await unitOfWork.CompleteAsync();

        return Result.Success();
    }
}
