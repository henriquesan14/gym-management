using Ardalis.Specification;
using GymManagementSystem.Domain.Abstractions;

namespace GymManagementSystem.Application.Shared.Contracts;

/// <summary>
/// Read-only Ardalis repository keyed by a strongly-typed id (<typeparamref name="TId"/>),
/// e.g. <c>MemberId</c>. Adds a type-safe <see cref="GetByIdAsync"/> on top of the
/// specification-based query surface from <see cref="IReadRepositoryBase{T}"/>.
/// </summary>
public interface IReadOnlyRepository<T, TId> : IReadRepositoryBase<T>
    where T : class, IAggregate<TId>
    where TId : notnull
{
    Task<T?> GetByIdAsync(TId id, CancellationToken cancellationToken = default);
}
