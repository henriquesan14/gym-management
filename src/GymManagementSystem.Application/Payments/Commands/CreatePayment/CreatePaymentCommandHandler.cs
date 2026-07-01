using GymManagementSystem.Application.Members;
using GymManagementSystem.Application.MembershipPlans;
using GymManagementSystem.Application.Shared.Contracts;
using GymManagementSystem.Domain.Members;
using GymManagementSystem.Domain.Members.Specifications;
using GymManagementSystem.Domain.MembershipPlans;
using GymManagementSystem.Domain.Payments;
using GymManagementSystem.Shared.Common.CRQS;
using GymManagementSystem.Shared.Common.ResultPattern;

namespace GymManagementSystem.Application.Payments.Commands.CreatePayment;

public sealed class CreatePaymentCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<CreatePaymentCommand, ResultT<PaymentResponse>>
{
    public async Task<ResultT<PaymentResponse>> Handle(CreatePaymentCommand request, CancellationToken ct)
    {
        var member = await unitOfWork.Members.SingleOrDefaultAsync(new MemberByIdWithMembershipsSpecification(request.MemberId), ct);
        if (member is null) return MemberErrors.NotFound(request.MemberId);

        var plan = await unitOfWork.MembershipPlans.GetByIdAsync(MembershipPlanId.Of(request.PlanId), ct);
        if (plan is null) return MembershipPlanErrors.NotFound(request.MemberId);

        Membership membership;

        if (member.HasActiveMembership)
        {
            membership = member.RenewMembership(plan);
        }
        else
        {
            membership = member.CreateMembership(
                plan,
                DateOnly.FromDateTime(DateTime.Today));
        }

        var payment = Payment.Create(
            PaymentId.New(),
            member.Id,
            membership.Id,
            plan.Price,
            request.PaymentMethod);

        await unitOfWork.Payments.AddAsync(payment, ct);

        await unitOfWork.CompleteAsync(ct);

        return payment.ToDto();
    }
}
