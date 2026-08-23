using Bfs.Core.Helpers;

using Bfs.StockEx.Contracts;
using Bfs.StockEx.Data.Interfaces;
using Bfs.StockEx.Domain.Interfaces;
using Bfs.StockEx.Domain.Mapper;

namespace Bfs.StockEx.Domain.Services
{
    public class ReportsService : IReportsService
    {
        private readonly ITradingRoomRepCompare _tradingRoomRepCompare;

        private readonly IPortfolioCompare _portfolioCompare;

        private readonly IPortfolioAggregateCompare _portfolioAggregateCompare;

        private readonly IPortfolioCashTransactionCompare _portfolioCashTransactionCompare;

        private readonly IPortfolioCashTransactionAggregateCompare _portfolioCashTransactionAggregateCompare;

//Template_Component_AddDeclareEntry
        public ReportsService(
              ITradingRoomRepCompare tradingRoomRepCompare

              ,IPortfolioCompare portfolioCompare

              ,IPortfolioAggregateCompare portfolioAggregateCompare

              ,IPortfolioCashTransactionCompare portfolioCashTransactionCompare

              ,IPortfolioCashTransactionAggregateCompare portfolioCashTransactionAggregateCompare

//Template_Component_AddParameterEntry
                            )
        {
              _tradingRoomRepCompare = tradingRoomRepCompare;

              _portfolioCompare = portfolioCompare;

              _portfolioAggregateCompare = portfolioAggregateCompare;

              _portfolioCashTransactionCompare = portfolioCashTransactionCompare;

              _portfolioCashTransactionAggregateCompare = portfolioCashTransactionAggregateCompare;

//Template_Component_AddInitEntry
        }

        public async Task<Bfs.Core.Contracts.QueryResponse<TradingRoomRepCompareItem>> TradingRoomRepCompareAsync(Bfs.Core.Contracts.QueryRequest<TradingRoomRepCompareFilter> contractRequest)
        {
            var entityRequest = SerializationHelper.DoMapping<Bfs.Core.Contracts.QueryRequest<TradingRoomRepCompareFilter>, Bfs.Core.Data.QueryRequest<Data.TradingRoomRepCompareFilter>>(contractRequest);

            var entityResult = await _tradingRoomRepCompare.GetAsync(entityRequest).ConfigureAwait(false);
            var mappedResult = SerializationHelper.DoMapping<Bfs.Core.Data.QueryResponse<Data.TradingRoomRepCompareItem>, Bfs.Core.Contracts.QueryResponse<TradingRoomRepCompareItem>>(entityResult);

            return mappedResult ?? new Bfs.Core.Contracts.QueryResponse<TradingRoomRepCompareItem> { Items = new List<TradingRoomRepCompareItem>(), TotalItems = 0, TotalPages = 0 };
        }

        public async Task<Bfs.Core.Contracts.QueryResponse<PortfolioCompareItem>> PortfolioCompareAsync(Bfs.Core.Contracts.QueryRequest<PortfolioCompareFilter> contractRequest)
        {
            var entityRequest = SerializationHelper.DoMapping<Bfs.Core.Contracts.QueryRequest<PortfolioCompareFilter>, Bfs.Core.Data.QueryRequest<Data.PortfolioCompareFilter>>(contractRequest);

            var entityResult = await _portfolioCompare.GetAsync(entityRequest).ConfigureAwait(false);
            var mappedResult = SerializationHelper.DoMapping<Bfs.Core.Data.QueryResponse<Data.PortfolioCompareItem>, Bfs.Core.Contracts.QueryResponse<PortfolioCompareItem>>(entityResult);

            return mappedResult ?? new Bfs.Core.Contracts.QueryResponse<PortfolioCompareItem> { Items = new List<PortfolioCompareItem>(), TotalItems = 0, TotalPages = 0 };
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
//Template_Component_AddServiceEntry
    }
}

