namespace GymManagementSystem.Shared.Common.Pagination;

public class PaginatedResult<TDto>
(int pageIndex, int pageSize, long count, IEnumerable<TDto> data)
where TDto : class
{
    public int PageIndex { get; } = pageIndex;
    public int PageSize { get; } = pageSize;
    public long Count { get; } = count;
    public IEnumerable<TDto> Data { get; } = data;
}
