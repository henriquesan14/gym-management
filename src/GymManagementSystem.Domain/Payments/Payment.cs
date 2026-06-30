using GymManagementSystem.Domain.Abstractions;
using GymManagementSystem.Domain.Enums;
using GymManagementSystem.Domain.Events;
using GymManagementSystem.Domain.Exceptions;
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
        Status = PaymentStatus.Pending;
        CreatedAt = DateTime.UtcNow;
    }

    public MemberId MemberId { get; private set; } = default!;
    public MembershipId MembershipId { get; private set; } = default!;

    public decimal Amount { get; private set; }

    public PaymentMethod Method { get; private set; }

    public PaymentStatus Status { get; private set; }

    public DateTime? PaidAt { get; private set; }

    public string? TransactionId { get; private set; }

    public static Payment Create(
        MemberId memberId,
        MembershipId membershipId,
        decimal amount,
        PaymentMethod method)
    {
        return new Payment(
            PaymentId.New(),
            memberId,
            membershipId,
            amount,
            method);
    }

    public void Complete(string transactionId)
    {
        if (Status != PaymentStatus.Pending)
            throw new DomainException("Only pending payments can be completed.");

        Status = PaymentStatus.Paid;
        PaidAt = DateTime.UtcNow;
        TransactionId = transactionId;

        AddDomainEvent(new PaymentCompletedEvent(
            Id,
            MemberId,
            MembershipId));
    }

    public void Fail()
    {
        if (Status != PaymentStatus.Pending)
            throw new DomainException("Only pending payments can fail.");

        Status = PaymentStatus.Failed;
    }

    public void Refund()
    {
        if (Status != PaymentStatus.Paid)
            throw new DomainException("Only paid payments can be refunded.");

        Status = PaymentStatus.Refunded;
    }
}
