using GymManagementSystem.Application.Shared.Contracts;
using GymManagementSystem.Shared.Common.CRQS;
using GymManagementSystem.Shared.Common.ResultPattern;

namespace GymManagementSystem.Application.Payments.Commands.CreatePayment;

public sealed class CreatePaymentCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<CreatePaymentCommand, ResultT<PaymentResponse>>
{
    public async Task<ResultT<PaymentResponse>> Handle(CreatePaymentCommand request, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
