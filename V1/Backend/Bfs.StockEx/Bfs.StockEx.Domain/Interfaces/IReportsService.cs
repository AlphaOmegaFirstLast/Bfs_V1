using Bfs.Core.Contracts;
using Bfs.StockEx.Contracts;

namespace Bfs.StockEx.Domain.Interfaces
{
    public interface IReportsService
    {

        Task<QueryResponse<TradingRoomRepCompareItem>> TradingRoomRepCompareAsync(QueryRequest<TradingRoomRepCompareFilter> contractRequest);

//Template_Component_AddIServiceEntry
  }
}
