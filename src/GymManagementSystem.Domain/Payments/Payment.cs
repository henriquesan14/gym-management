using GymManagementSystem.Domain.Abstractions;
using GymManagementSystem.Domain.Enums;
using GymManagementSystem.Domain.Members;

namespace GymManagementSystem.Domain.Payments;

public sealed class Payment : Aggregate<PaymentId>
{
    private Payment()
    {
    }

    private Payment(
        PaymentId id,
        MemberId memberId,
        MembershipId membershipId,
        decimal amount,
        PaymentMethod method)
    {
        Id = id;
        MemberId = memberId;
        MembershipId = membershipId;
        Amount = amount;
        Method = method;
    }

    public MemberId MemberId { get; private set; } = default!;
    public MembershipId MembershipId { get; private set; } = default!;

    public decimal Amount { get; private set; }

    public PaymentMethod Method { get; private set; }

    public DateTime? PaidAt { get; private set; }

    public string? TransactionId { get; private set; }

    public static Payment Create(
        PaymentId id,
        MemberId memberId,
        MembershipId membershipId,
        decimal amount,
        PaymentMethod method)
    {
        return new Payment(
            id,
            memberId,
            membershipId,
            amount,
            method);
    }
}
