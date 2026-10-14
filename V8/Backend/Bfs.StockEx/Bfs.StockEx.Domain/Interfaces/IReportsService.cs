using Bfs.Core.Contracts;
using Bfs.StockEx.Contracts;
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

namespace Bfs.StockEx.Domain.Interfaces
{
    public interface IReportsService
    {

        Task<QueryResponse<TradingRoomRepCompareItem>> TradingRoomRepCompareAsync(QueryRequest<TradingRoomRepCompareFilter> contractRequest);

        Task<QueryResponse<PortfolioStockShareTransactionCompareItem>> PortfolioStockShareTransactionCompareAsync(QueryRequest<PortfolioStockShareTransactionCompareFilter> contractRequest);

        Task<QueryResponse<PortfolioAggregateCompareItem>> PortfolioAggregateCompareAsync(QueryRequest<PortfolioAggregateCompareFilter> contractRequest);

        Task<QueryResponse<PortfolioCashTransactionCompareItem>> PortfolioCashTransactionCompareAsync(QueryRequest<PortfolioCashTransactionCompareFilter> contractRequest);

        Task<QueryResponse<PortfolioCashTransactionAggregateCompareItem>> PortfolioCashTransactionAggregateCompareAsync(QueryRequest<PortfolioCashTransactionAggregateCompareFilter> contractRequest);

        Task<QueryResponse<NavReportCompareItem>> NavReportCompareAsync(QueryRequest<NavReportCompareFilter> contractRequest);

        Task<QueryResponse<OverdraftRepCompareItem>> OverdraftRepCompareAsync(QueryRequest<OverdraftRepCompareFilter> contractRequest);

//Template_Component_AddIServiceEntry
//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

  }
}
