using Bfs.Core.Data;
using Bfs.StockEx.Data;

namespace Bfs.StockEx.Data.Interfaces
{
    public interface ITransferCostTypeList
    {
        Task<QueryResponse<TransferCostTypeListItem>> GetAsync(QueryRequest<TransferCostTypeListFilter> request);
    }
}

