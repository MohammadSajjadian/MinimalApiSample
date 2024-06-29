namespace Shared.Extensions;

public static class PagingExtension
{
    public static IQueryable<TSource> Paging<TSource>(this IQueryable<TSource> source, int? pageIndex, int? pageSize)
    {
        ArgumentNullException.ThrowIfNull(source);

        int tempPageIndex = (int)(pageIndex is null ? 1 : pageIndex);
        int tempPageSize = (int)(pageSize is null ? 5 : pageSize);

        return source
            .Skip((tempPageIndex - 1) * tempPageSize)
            .Take(tempPageSize);
    }
}
