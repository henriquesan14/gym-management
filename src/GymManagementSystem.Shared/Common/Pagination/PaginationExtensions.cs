using Ardalis.Specification;

namespace GymManagementSystem.Shared.Common.Pagination;
public static class PaginationExtensions
{
    public static async Task<PaginatedResult<TDto>> ToPaginatedListAsync<TEntity, TDto>(
        this IReadRepositoryBase<TEntity> repository,
        ISpecification<TEntity> spec,
        int page,
        int pageSize,
        Func<TEntity, TDto> selector)
        where TEntity : class
        where TDto : class
    {
        var total = await repository.CountAsync(spec);

        var pagedSpec = spec;
        if (pagedSpec is Specification<TEntity> s)
        {
            s.Query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        var items = (await repository.ListAsync(pagedSpec))
            .Select(selector)
            .ToList();

        return new PaginatedResult<TDto>(page, pageSize, total, items);
    }
}
