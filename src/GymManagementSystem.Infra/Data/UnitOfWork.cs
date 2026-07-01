using GymManagementSystem.Application.Auth;
using GymManagementSystem.Application.Members;
using GymManagementSystem.Application.MembershipPlans;
using GymManagementSystem.Application.Payments;
using GymManagementSystem.Application.Shared.Contracts;
using GymManagementSystem.Application.Users;
using Microsoft.EntityFrameworkCore.Storage;

namespace GymManagementSystem.Infra.Data;

public sealed class UnitOfWork : IUnitOfWork, IAsyncDisposable
{
    private IDbContextTransaction? _transaction;
    private readonly GymManagementDbContext _dbContext;

    public UnitOfWork(GymManagementDbContext dbContext, IMemberRepository members, IUserRepository users, IRefreshTokenRepository refreshTokens, IMembershipPlanRepository membershipPlans, IPaymentRepository payments)
    {
        _dbContext = dbContext;
        Members = members;
        Users = users;
        RefreshTokens = refreshTokens;
        MembershipPlans = membershipPlans;
        Payments = payments;
    }

    public IMemberRepository Members { get; }
    public IUserRepository Users { get; }
    public IRefreshTokenRepository RefreshTokens { get; }

    public IMembershipPlanRepository MembershipPlans { get; }

    public IPaymentRepository Payments { get; }

    public async Task BeginTransaction(CancellationToken ct)
    {
        if (_transaction is not null)
            throw new InvalidOperationException("A transaction is already active.");

        _transaction = await _dbContext.Database.BeginTransactionAsync(ct);
    }

    public async Task CommitAsync(CancellationToken ct)
    {
        if (_transaction is null)
            throw new InvalidOperationException("No active transaction.");

        try
        {
            await _transaction.CommitAsync(ct);
        }
        catch
        {
            await _transaction.RollbackAsync(ct);
            throw;
        }
        finally
        {
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public async Task<int> CompleteAsync(CancellationToken ct)
    {
        return await _dbContext.SaveChangesAsync(ct);
    }

    public async ValueTask DisposeAsync()
    {
        if (_transaction is not null)
            await _transaction.DisposeAsync();

        await _dbContext.DisposeAsync();
    }
}
