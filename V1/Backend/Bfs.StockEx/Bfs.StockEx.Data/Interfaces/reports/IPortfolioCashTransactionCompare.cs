using Bfs.Core.Data;
using Bfs.StockEx.Data;

namespace Bfs.StockEx.Data.Interfaces
{
    public interface IPortfolioCashTransactionCompare
    {
        Task<QueryResponse<PortfolioCashTransactionCompareItem>> GetAsync(QueryRequest<PortfolioCashTransactionCompareFilter> request);
    }
}