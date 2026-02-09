namespace Bfs.Core.Contracts;

public class QueryResponse<T>
{
    public List<T> Items { get; set; } = new();
    public long TotalItems { get; set; } = 0;
    public long TotalPages { get; set; } = 0;
}