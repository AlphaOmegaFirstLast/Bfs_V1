namespace Bfs.Core.Contracts;

public class QueryRequest<TFilter> where TFilter : new()
{
    public TFilter Filter { get; set; } = new();
    public SortOption? SortOption { get; set; }
    public int? PageIndex { get; set; } = 1;
    public int? PageSize { get; set; } = 10;
}