using Bfs.Core.Contracts;
using Bfs.StockEx.Contracts;

namespace Bfs.StockEx.Domain.Interfaces
{
    public interface IReportsService
    {

        Task<QueryResponse<TradingRoomRepCompareItem>> TradingRoomRepCompareAsync(QueryRequest<TradingRoomRepCompareFilter> contractRequest);

        Task<QueryResponse<PortfolioCompareItem>> PortfolioCompareAsync(QueryRequest<PortfolioCompareFilter> contractRequest);

        Task<QueryResponse<PortfolioAggregateCompareItem>> PortfolioAggregateCompareAsync(QueryRequest<PortfolioAggregateCompareFilter> contractRequest);

        Task<QueryResponse<PortfolioCashTransactionCompareItem>> PortfolioCashTransactionCompareAsync(QueryRequest<PortfolioCashTransactionCompareFilter> contractRequest);

        Task<QueryResponse<PortfolioCashTransactionAggregateCompareItem>> PortfolioCashTransactionAggregateCompareAsync(QueryRequest<PortfolioCashTransactionAggregateCompareFilter> contractRequest);

        Task<QueryResponse<ReportCompareItem>> ReportCompareAsync(QueryRequest<ReportCompareFilter> contractRequest);

        Task<QueryResponse<NavReportCompareItem>> NavReportCompareAsync(QueryRequest<NavReportCompareFilter> contractRequest);

//Template_Component_AddIServiceEntry
  }
}
