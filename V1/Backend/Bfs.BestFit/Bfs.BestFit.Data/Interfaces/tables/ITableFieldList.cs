using Bfs.Core.Data;
using Bfs.BestFit.Data;

namespace Bfs.BestFit.Data.Interfaces
{
    public interface ITableFieldList
    {
        Task<QueryResponse<TableFieldListItem>> GetTableFieldListAsync(QueryRequest<TableFieldListFilter> request);
    }
}