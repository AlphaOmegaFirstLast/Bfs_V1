using Bfs.Core.Data;
using Bfs.StockEx.Data;

namespace Bfs.StockEx.Data.Interfaces
{
    public interface IProcessStatusList
    {
        Task<QueryResponse<ProcessStatusListItem>> GetAsync(QueryRequest<ProcessStatusListFilter> request);
    }
}