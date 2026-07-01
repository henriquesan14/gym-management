using GymManagementSystem.Domain.Enums;

namespace GymManagementSystem.Application.Payments;

public sealed record PaymentResponse(Guid Id, Guid MemberId, Guid MembershipId, decimal Amount, PaymentMethod PaymentMethod, DateTime? PaidAt, string? TransactionId, DateTime? CreatedAt, string? CreatedByName);
