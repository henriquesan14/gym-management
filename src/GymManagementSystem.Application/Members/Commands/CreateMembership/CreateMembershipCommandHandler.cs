using GymManagementSystem.Application.Shared.Contracts;
using GymManagementSystem.Domain.Members;
using GymManagementSystem.Shared.Common.CRQS;
using GymManagementSystem.Shared.Common.ResultPattern;

namespace GymManagementSystem.Application.Members.Commands.CreateMembership;

internal class CreateMembershipCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<CreateMembershipCommand, Result>
{
    public async Task<Result> Handle(CreateMembershipCommand request, CancellationToken cancellationToken)
    {
        var member = await unitOfWork.Members.GetByIdAsync(MemberId.Of(request.MemberId));
        if (member is null) return MemberErrors.NotFound(request.MemberId);

        member.CreateMembership(request.StartDate, request.DurationInMonths);
        await unitOfWork.CompleteAsync();

        return Result.Success();
    }
}
