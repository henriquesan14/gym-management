using GymManagementSystem.Application.Members;
using GymManagementSystem.Application.Shared.Contracts;
using Microsoft.EntityFrameworkCore.Storage;

namespace GymManagementSystem.Infra.Data;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private IDbContextTransaction _transaction;
    private readonly GymManagementDbContext _dbContext;

    public UnitOfWork(GymManagementDbContext dbContext, IMemberRepository members)
    {
        _dbContext = dbContext;
        Members = members;
    }

    public IMemberRepository Members { get; }

    public async Task BeginTransaction()
    {
        _transaction = await _dbContext.Database.BeginTransactionAsync();
    }

    public async Task CommitAsync()
    {
        try
        {
            await _transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            await _transaction.RollbackAsync();
            throw ex;
        }
    }

    public async Task<int> CompleteAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        IsDisposing(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void IsDisposing(bool disposing)
    {
        if (disposing)
        {
            _transaction?.Dispose();
            _dbContext.Dispose();
        }
    }
}
