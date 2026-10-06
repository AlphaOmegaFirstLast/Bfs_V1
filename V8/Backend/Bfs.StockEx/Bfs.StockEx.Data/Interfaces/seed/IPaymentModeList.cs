using Bfs.Core.Data;
using Bfs.StockEx.Data;

namespace Bfs.StockEx.Data.Interfaces
{
    public interface IPaymentModeList
    {
        Task<QueryResponse<PaymentModeListItem>> GetAsync(QueryRequest<PaymentModeListFilter> request);
    }
}