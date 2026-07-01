using GymManagementSystem.Application.Payments;
using GymManagementSystem.Domain.Payments;

namespace GymManagementSystem.Infra.Data.Repositories;

public sealed class PaymentRepository : Repository<Payment, PaymentId>, IPaymentRepository
{
    public PaymentRepository(GymManagementDbContext db) : base(db)
    {
    }
}
