using GymManagementSystem.Application.Shared.Contracts;
using GymManagementSystem.Domain.Payments;

namespace GymManagementSystem.Application.Payments;

public interface IPaymentRepository : IRepository<Payment, PaymentId>;
