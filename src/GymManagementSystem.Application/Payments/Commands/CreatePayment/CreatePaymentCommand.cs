using GymManagementSystem.Domain.Enums;
using GymManagementSystem.Shared.Common.CRQS;
using GymManagementSystem.Shared.Common.ResultPattern;

namespace GymManagementSystem.Application.Payments.Commands.CreatePayment;

public sealed record CreatePaymentCommand(Guid MemberId, Guid PlanId, PaymentMethod PaymentMethod) : ICommand<ResultT<PaymentResponse>>;
