using Bfs.Core.Data;
using Bfs.StockEx.Data;

namespace Bfs.StockEx.Data.Interfaces
{
    public interface IInvestorList
    {
        Task<QueryResponse<InvestorListItem>> GetAsync(QueryRequest<InvestorListFilter> request);
    }
}