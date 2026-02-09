namespace Bfs.Core.Data;

public class QueryRequest<TFilter> : QueryBaseRequest where TFilter : new()
{
    public TFilter Filter { get; set; } = new();
}