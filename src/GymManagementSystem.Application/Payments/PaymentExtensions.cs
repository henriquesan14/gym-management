using GymManagementSystem.Domain.Payments;

namespace GymManagementSystem.Application.Payments;

public static class PaymentExtensions
{
    public static PaymentResponse ToDto(this Payment payment)
    {
        return new PaymentResponse(
            payment.Id.Value,
            payment.MemberId.Value,
            payment.MembershipId.Value,
            payment.Amount,
            payment.Method,
            payment.PaidAt,
            payment.TransactionId,
            payment.CreatedAt,
            payment.CreatedByName
        );
    }

    public static List<PaymentResponse> ToDto(this IEnumerable<Payment> payments)
    {
        return payments
            .Select(ToDto)
            .ToList();
    }
}
