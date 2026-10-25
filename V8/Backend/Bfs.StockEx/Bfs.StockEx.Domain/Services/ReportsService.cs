using Bfs.Core.Helpers;

using Bfs.StockEx.Contracts;
using Bfs.StockEx.Data.Interfaces;
using Bfs.StockEx.Domain.Interfaces;
using Bfs.StockEx.Domain.Mapper;
//Template_Start_Code_DontOverwrite_1
//Template_End_Code_DontOverwrite_1

namespace Bfs.StockEx.Domain.Services
{
    public class ReportsService : IReportsService
    {
        private readonly ITradingRoomRepCompare _tradingRoomRepCompare;

        private readonly IPortfolioStockShareTransactionCompare _portfolioStockShareTransactionCompare;

        private readonly IPortfolioAggregateCompare _portfolioAggregateCompare;

        private readonly IPortfolioCashTransactionCompare _portfolioCashTransactionCompare;

        private readonly IPortfolioCashTransactionAggregateCompare _portfolioCashTransactionAggregateCompare;

        private readonly INavReportCompare _navReportCompare;

        private readonly IOverdraftRepCompare _overdraftRepCompare;

        private readonly IStockshareBrokerRepCompare _stockshareBrokerRepCompare;

        private readonly IPortfolioProfitRepCompare _portfolioProfitRepCompare;

//Template_Component_AddDeclareEntry
//Template_Start_Code_DontOverwrite_2
//Template_End_Code_DontOverwrite_2

        public ReportsService(
              ITradingRoomRepCompare tradingRoomRepCompare

              ,IPortfolioStockShareTransactionCompare portfolioStockShareTransactionCompare

              ,IPortfolioAggregateCompare portfolioAggregateCompare

              ,IPortfolioCashTransactionCompare portfolioCashTransactionCompare

              ,IPortfolioCashTransactionAggregateCompare portfolioCashTransactionAggregateCompare

              ,INavReportCompare navReportCompare

              ,IOverdraftRepCompare overdraftRepCompare

              ,IStockshareBrokerRepCompare stockshareBrokerRepCompare

              ,IPortfolioProfitRepCompare portfolioProfitRepCompare

//Template_Component_AddParameterEntry
//Template_Start_Code_DontOverwrite_3
//Template_End_Code_DontOverwrite_3

                            )
        {
              _tradingRoomRepCompare = tradingRoomRepCompare;

              _portfolioStockShareTransactionCompare = portfolioStockShareTransactionCompare;

              _portfolioAggregateCompare = portfolioAggregateCompare;

              _portfolioCashTransactionCompare = portfolioCashTransactionCompare;

              _portfolioCashTransactionAggregateCompare = portfolioCashTransactionAggregateCompare;

              _navReportCompare = navReportCompare;

              _overdraftRepCompare = overdraftRepCompare;

              _stockshareBrokerRepCompare = stockshareBrokerRepCompare;

              _portfolioProfitRepCompare = portfolioProfitRepCompare;

//Template_Component_AddInitEntry
//Template_Start_Code_DontOverwrite_4
//Template_End_Code_DontOverwrite_4

        }

        public async Task<Bfs.Core.Contracts.QueryResponse<TradingRoomRepCompareItem>> TradingRoomRepCompareAsync(Bfs.Core.Contracts.QueryRequest<TradingRoomRepCompareFilter> contractRequest)
        {
            var entityRequest = SerializationHelper.DoMapping<Bfs.Core.Contracts.QueryRequest<TradingRoomRepCompareFilter>, Bfs.Core.Data.QueryRequest<Data.TradingRoomRepCompareFilter>>(contractRequest);

            var entityResult = await _tradingRoomRepCompare.GetAsync(entityRequest).ConfigureAwait(false);
            var mappedResult = SerializationHelper.DoMapping<Bfs.Core.Data.QueryResponse<Data.TradingRoomRepCompareItem>, Bfs.Core.Contracts.QueryResponse<TradingRoomRepCompareItem>>(entityResult);

            return mappedResult ?? new Bfs.Core.Contracts.QueryResponse<TradingRoomRepCompareItem> { Items = new List<TradingRoomRepCompareItem>(), TotalItems = 0, TotalPages = 0 };
        }

        public async Task<Bfs.Core.Contracts.QueryResponse<PortfolioStockShareTransactionCompareItem>> PortfolioStockShareTransactionCompareAsync(Bfs.Core.Contracts.QueryRequest<PortfolioStockShareTransactionCompareFilter> contractRequest)
        {
            var entityRequest = SerializationHelper.DoMapping<Bfs.Core.Contracts.QueryRequest<PortfolioStockShareTransactionCompareFilter>, Bfs.Core.Data.QueryRequest<Data.PortfolioStockShareTransactionCompareFilter>>(contractRequest);

            var entityResult = await _portfolioStockShareTransactionCompare.GetAsync(entityRequest).ConfigureAwait(false);
            var mappedResult = SerializationHelper.DoMapping<Bfs.Core.Data.QueryResponse<Data.PortfolioStockShareTransactionCompareItem>, Bfs.Core.Contracts.QueryResponse<PortfolioStockShareTransactionCompareItem>>(entityResult);

            return mappedResult ?? new Bfs.Core.Contracts.QueryResponse<PortfolioStockShareTransactionCompareItem> { Items = new List<PortfolioStockShareTransactionCompareItem>(), TotalItems = 0, TotalPages = 0 };
        }

        public async Task<Bfs.Core.Contracts.QueryResponse<PortfolioAggregateCompareItem>> PortfolioAggregateCompareAsync(Bfs.Core.Contracts.QueryRequest<PortfolioAggregateCompareFilter> contractRequest)
        {
            var entityRequest = SerializationHelper.DoMapping<Bfs.Core.Contracts.QueryRequest<PortfolioAggregateCompareFilter>, Bfs.Core.Data.QueryRequest<Data.PortfolioAggregateCompareFilter>>(contractRequest);

            var entityResult = await _portfolioAggregateCompare.GetAsync(entityRequest).ConfigureAwait(false);
            var mappedResult = SerializationHelper.DoMapping<Bfs.Core.Data.QueryResponse<Data.PortfolioAggregateCompareItem>, Bfs.Core.Contracts.QueryResponse<PortfolioAggregateCompareItem>>(entityResult);

            return mappedResult ?? new Bfs.Core.Contracts.QueryResponse<PortfolioAggregateCompareItem> { Items = new List<PortfolioAggregateCompareItem>(), TotalItems = 0, TotalPages = 0 };
        }

        public async Task<Bfs.Core.Contracts.QueryResponse<PortfolioCashTransactionCompareItem>> PortfolioCashTransactionCompareAsync(Bfs.Core.Contracts.QueryRequest<PortfolioCashTransactionCompareFilter> contractRequest)
        {
            var entityRequest = SerializationHelper.DoMapping<Bfs.Core.Contracts.QueryRequest<PortfolioCashTransactionCompareFilter>, Bfs.Core.Data.QueryRequest<Data.PortfolioCashTransactionCompareFilter>>(contractRequest);

            var entityResult = await _portfolioCashTransactionCompare.GetAsync(entityRequest).ConfigureAwait(false);
            var mappedResult = SerializationHelper.DoMapping<Bfs.Core.Data.QueryResponse<Data.PortfolioCashTransactionCompareItem>, Bfs.Core.Contracts.QueryResponse<PortfolioCashTransactionCompareItem>>(entityResult);

            return mappedResult ?? new Bfs.Core.Contracts.QueryResponse<PortfolioCashTransactionCompareItem> { Items = new List<PortfolioCashTransactionCompareItem>(), TotalItems = 0, TotalPages = 0 };
        }

        public async Task<Bfs.Core.Contracts.QueryResponse<PortfolioCashTransactionAggregateCompareItem>> PortfolioCashTransactionAggregateCompareAsync(Bfs.Core.Contracts.QueryRequest<PortfolioCashTransactionAggregateCompareFilter> contractRequest)
        {
            var entityRequest = SerializationHelper.DoMapping<Bfs.Core.Contracts.QueryRequest<PortfolioCashTransactionAggregateCompareFilter>, Bfs.Core.Data.QueryRequest<Data.PortfolioCashTransactionAggregateCompareFilter>>(contractRequest);

            var entityResult = await _portfolioCashTransactionAggregateCompare.GetAsync(entityRequest).ConfigureAwait(false);
            var mappedResult = SerializationHelper.DoMapping<Bfs.Core.Data.QueryResponse<Data.PortfolioCashTransactionAggregateCompareItem>, Bfs.Core.Contracts.QueryResponse<PortfolioCashTransactionAggregateCompareItem>>(entityResult);

            return mappedResult ?? new Bfs.Core.Contracts.QueryResponse<PortfolioCashTransactionAggregateCompareItem> { Items = new List<PortfolioCashTransactionAggregateCompareItem>(), TotalItems = 0, TotalPages = 0 };
        }

        public async Task<Bfs.Core.Contracts.QueryResponse<NavReportCompareItem>> NavReportCompareAsync(Bfs.Core.Contracts.QueryRequest<NavReportCompareFilter> contractRequest)
        {
            var entityRequest = SerializationHelper.DoMapping<Bfs.Core.Contracts.QueryRequest<NavReportCompareFilter>, Bfs.Core.Data.QueryRequest<Data.NavReportCompareFilter>>(contractRequest);

            var entityResult = await _navReportCompare.GetAsync(entityRequest).ConfigureAwait(false);
            var mappedResult = SerializationHelper.DoMapping<Bfs.Core.Data.QueryResponse<Data.NavReportCompareItem>, Bfs.Core.Contracts.QueryResponse<NavReportCompareItem>>(entityResult);

            return mappedResult ?? new Bfs.Core.Contracts.QueryResponse<NavReportCompareItem> { Items = new List<NavReportCompareItem>(), TotalItems = 0, TotalPages = 0 };
        }

        public async Task<Bfs.Core.Contracts.QueryResponse<OverdraftRepCompareItem>> OverdraftRepCompareAsync(Bfs.Core.Contracts.QueryRequest<OverdraftRepCompareFilter> contractRequest)
        {
            var entityRequest = SerializationHelper.DoMapping<Bfs.Core.Contracts.QueryRequest<OverdraftRepCompareFilter>, Bfs.Core.Data.QueryRequest<Data.OverdraftRepCompareFilter>>(contractRequest);

            var entityResult = await _overdraftRepCompare.GetAsync(entityRequest).ConfigureAwait(false);
            var mappedResult = SerializationHelper.DoMapping<Bfs.Core.Data.QueryResponse<Data.OverdraftRepCompareItem>, Bfs.Core.Contracts.QueryResponse<OverdraftRepCompareItem>>(entityResult);

            return mappedResult ?? new Bfs.Core.Contracts.QueryResponse<OverdraftRepCompareItem> { Items = new List<OverdraftRepCompareItem>(), TotalItems = 0, TotalPages = 0 };
        }

        public async Task<Bfs.Core.Contracts.QueryResponse<StockshareBrokerRepCompareItem>> StockshareBrokerRepCompareAsync(Bfs.Core.Contracts.QueryRequest<StockshareBrokerRepCompareFilter> contractRequest)
        {
            var entityRequest = SerializationHelper.DoMapping<Bfs.Core.Contracts.QueryRequest<StockshareBrokerRepCompareFilter>, Bfs.Core.Data.QueryRequest<Data.StockshareBrokerRepCompareFilter>>(contractRequest);

            var entityResult = await _stockshareBrokerRepCompare.GetAsync(entityRequest).ConfigureAwait(false);
            var mappedResult = SerializationHelper.DoMapping<Bfs.Core.Data.QueryResponse<Data.StockshareBrokerRepCompareItem>, Bfs.Core.Contracts.QueryResponse<StockshareBrokerRepCompareItem>>(entityResult);

            return mappedResult ?? new Bfs.Core.Contracts.QueryResponse<StockshareBrokerRepCompareItem> { Items = new List<StockshareBrokerRepCompareItem>(), TotalItems = 0, TotalPages = 0 };
        }

        public async Task<Bfs.Core.Contracts.QueryResponse<PortfolioProfitRepCompareItem>> PortfolioProfitRepCompareAsync(Bfs.Core.Contracts.QueryRequest<PortfolioProfitRepCompareFilter> contractRequest)
        {
            var entityRequest = SerializationHelper.DoMapping<Bfs.Core.Contracts.QueryRequest<PortfolioProfitRepCompareFilter>, Bfs.Core.Data.QueryRequest<Data.PortfolioProfitRepCompareFilter>>(contractRequest);

            var entityResult = await _portfolioProfitRepCompare.GetAsync(entityRequest).ConfigureAwait(false);
            var mappedResult = SerializationHelper.DoMapping<Bfs.Core.Data.QueryResponse<Data.PortfolioProfitRepCompareItem>, Bfs.Core.Contracts.QueryResponse<PortfolioProfitRepCompareItem>>(entityResult);

            return mappedResult ?? new Bfs.Core.Contracts.QueryResponse<PortfolioProfitRepCompareItem> { Items = new List<PortfolioProfitRepCompareItem>(), TotalItems = 0, TotalPages = 0 };
        }
//Template_Component_AddServiceEntry
//Template_Start_Code_DontOverwrite_5
//Template_End_Code_DontOverwrite_5

    }
}

