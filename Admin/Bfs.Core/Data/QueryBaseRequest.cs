namespace Bfs.Core.Data;

public class QueryBaseRequest
{
    public SortOption? SortOption { get; set; }
    public int? PageIndex { get; set; } = 1;
    public int? PageSize { get; set; } = 10;
}