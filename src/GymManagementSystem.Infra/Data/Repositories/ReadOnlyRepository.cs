using Ardalis.Specification.EntityFrameworkCore;
using GymManagementSystem.Application.Shared.Contracts;
using GymManagementSystem.Domain.Abstractions;

namespace GymManagementSystem.Infra.Data.Repositories;

public class ReadOnlyRepository<T, TId>(GymManagementDbContext db)
    : RepositoryBase<T>(db), IReadOnlyRepository<T, TId>
    where T : class, IAggregate<TId>
    where TId : notnull
{
    public async Task<T?> GetByIdAsync(TId id, CancellationToken cancellationToken = default)
        => await GetByIdAsync<TId>(id, cancellationToken);
}
