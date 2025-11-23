namespace Roleover.Application.DTOs;

public class PaginatedResult<T>(IEnumerable<T> items, int page, int pageSize, int totalItems)
{
    public IEnumerable<T> Items { get; } = items;
    public int Page { get; } = page;
    public int PageSize { get; } = pageSize;
    public int TotalItems { get; } = totalItems;
    public int TotalPages { get; } = pageSize == 0 ? 0 : (int)Math.Ceiling(totalItems / (double)pageSize);
}