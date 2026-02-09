using Bfs.Core.Data;
using Bfs.BestFit.Data;

namespace Bfs.BestFit.Data.Interfaces
{
    public interface IBusinessActionList
    {
        Task<QueryResponse<BusinessActionListItem>> GetBusinessActionListAsync(QueryRequest<BusinessActionListFilter> request);
    }
}