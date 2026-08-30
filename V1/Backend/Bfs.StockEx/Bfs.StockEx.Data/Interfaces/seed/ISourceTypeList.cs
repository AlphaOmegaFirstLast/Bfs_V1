using Bfs.Core.Data;
using Bfs.StockEx.Data;

namespace Bfs.StockEx.Data.Interfaces
{
    public interface ISourceTypeList
    {
        Task<QueryResponse<SourceTypeListItem>> GetAsync(QueryRequest<SourceTypeListFilter> request);
    }
}

