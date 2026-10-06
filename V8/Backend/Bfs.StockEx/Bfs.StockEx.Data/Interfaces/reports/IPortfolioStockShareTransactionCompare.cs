using Bfs.Core.Data;
using Bfs.StockEx.Data;
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

namespace Bfs.StockEx.Data.Interfaces
{
    public interface IPortfolioStockShareTransactionCompare
    {
        Task<QueryResponse<PortfolioStockShareTransactionCompareItem>> GetAsync(QueryRequest<PortfolioStockShareTransactionCompareFilter> request);
//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2
    }
}