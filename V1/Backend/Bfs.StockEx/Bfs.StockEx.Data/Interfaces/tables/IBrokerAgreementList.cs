using Bfs.Core.Data;
using Bfs.StockEx.Data;

namespace Bfs.StockEx.Data.Interfaces
{
    public interface IBrokerAgreementList
    {
        Task<QueryResponse<BrokerAgreementListItem>> GetAsync(QueryRequest<BrokerAgreementListFilter> request);
    }
}