using GymManagementSystem.Shared.Common.CRQS;
using GymManagementSystem.Shared.Common.ResultPattern;

namespace GymManagementSystem.Application.Payments.Commands.CreatePayment;

public sealed record CreatePaymentCommand : ICommand<ResultT<PaymentResponse>>;
