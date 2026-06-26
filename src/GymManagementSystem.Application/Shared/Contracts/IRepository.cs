using Ardalis.Specification;
using GymManagementSystem.Domain.Abstractions;

namespace GymManagementSystem.Application.Shared.Contracts;

/// <summary>
/// Read/write Ardalis repository keyed by a strongly-typed id (<typeparamref name="TId"/>).
/// Inherits the full <see cref="IRepositoryBase{T}"/> surface plus the type-safe
/// <see cref="IReadOnlyRepository{T,TId}.GetByIdAsync"/>.
/// </summary>
public interface IRepository<T, TId> : IRepositoryBase<T>, IReadOnlyRepository<T, TId>
    where T : class, IAggregate<TId>
    where TId : notnull;
