using GymManagementSystem.Application.Shared.Contracts;
using GymManagementSystem.Domain.Members.Specifications;
using GymManagementSystem.Shared.Common.CRQS;
using GymManagementSystem.Shared.Common.ResultPattern;

namespace GymManagementSystem.Application.Members.Commands.RenewMembership;

public sealed class RenewMembershipCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<RenewMembershipCommand, Result>
{
    public async Task<Result> Handle(RenewMembershipCommand request, CancellationToken ct)
    {
        var member = await unitOfWork.Members.SingleOrDefaultAsync(new MemberByIdWithMembershipsSpecification(request.MemberId), ct);

        if (member is null)
            return MemberErrors.NotFound(request.MemberId);

        member.RenewMembership(request.Months);

        await unitOfWork.CompleteAsync(ct);

        return Result.Success();
    }
}
