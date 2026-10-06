using Bfs.Core.Data;
using Bfs.Master.Data;

namespace Bfs.Master.Data.Interfaces
{
    public interface IBusinessActionList
    {
        Task<QueryResponse<BusinessActionListItem>> GetAsync(QueryRequest<BusinessActionListFilter> request);
    }
}