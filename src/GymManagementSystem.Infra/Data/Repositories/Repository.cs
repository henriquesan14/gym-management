using Ardalis.Specification.EntityFrameworkCore;
using GymManagementSystem.Application.Shared.Contracts;
using GymManagementSystem.Domain.Abstractions;

namespace GymManagementSystem.Infra.Data.Repositories;

public class Repository<T, TId>(GymManagementDbContext db)
    : RepositoryBase<T>(db), IRepository<T, TId>
    where T : class, IAggregate<TId>
    where TId : notnull
{
    public async Task<T?> GetByIdAsync(TId id, CancellationToken cancellationToken = default)
        => await GetByIdAsync<TId>(id, cancellationToken);
}
