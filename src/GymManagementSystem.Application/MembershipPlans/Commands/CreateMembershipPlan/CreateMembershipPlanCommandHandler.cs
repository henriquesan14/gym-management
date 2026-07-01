using GymManagementSystem.Application.Shared.Contracts;
using GymManagementSystem.Domain.MembershipPlans;
using GymManagementSystem.Shared.Common.CRQS;
using GymManagementSystem.Shared.Common.ResultPattern;

namespace GymManagementSystem.Application.MembershipPlans.Commands.CreateMembershipPlan;

public sealed class CreateMembershipPlanCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<CreateMembershipPlanCommand, ResultT<MembershipPlanResponse>>
{
    public async Task<ResultT<MembershipPlanResponse>> Handle(CreateMembershipPlanCommand request, CancellationToken ct)
    {
        var nameExist = await unitOfWork.MembershipPlans.NameExistsAsync(request.Name);
        if (nameExist) return MembershipPlanErrors.Conflict(request.Name);

        var membership = MembershipPlan.Create(MembershipPlanId.New(), request.Name, request.Price, request.DurationInDays);

        await unitOfWork.MembershipPlans.AddAsync(membership, ct);
        await unitOfWork.CompleteAsync(ct);

        return membership.ToDto();
    }
}
