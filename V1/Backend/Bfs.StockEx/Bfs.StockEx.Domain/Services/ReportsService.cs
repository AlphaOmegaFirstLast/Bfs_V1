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

//Template_Component_AddDeclareEntry
        public ReportsService(
              ITradingRoomRepCompare tradingRoomRepCompare
//Template_Component_AddParameterEntry
                            )
        {
              _tradingRoomRepCompare = tradingRoomRepCompare;

//Template_Component_AddInitEntry
        }

        public async Task<Bfs.Core.Contracts.QueryResponse<TradingRoomRepCompareItem>> TradingRoomRepCompareAsync(Bfs.Core.Contracts.QueryRequest<TradingRoomRepCompareFilter> contractRequest)
        {
            var entityRequest = SerializationHelper.DoMapping<Bfs.Core.Contracts.QueryRequest<TradingRoomRepCompareFilter>, Bfs.Core.Data.QueryRequest<Data.TradingRoomRepCompareFilter>>(contractRequest);

            var entityResult = await _tradingRoomRepCompare.GetAsync(entityRequest).ConfigureAwait(false);
            var mappedResult = SerializationHelper.DoMapping<Bfs.Core.Data.QueryResponse<Data.TradingRoomRepCompareItem>, Bfs.Core.Contracts.QueryResponse<TradingRoomRepCompareItem>>(entityResult);

            return mappedResult ?? new Bfs.Core.Contracts.QueryResponse<TradingRoomRepCompareItem> { Items = new List<TradingRoomRepCompareItem>(), TotalItems = 0, TotalPages = 0 };
        }
//Template_Component_AddServiceEntry
    }
}

