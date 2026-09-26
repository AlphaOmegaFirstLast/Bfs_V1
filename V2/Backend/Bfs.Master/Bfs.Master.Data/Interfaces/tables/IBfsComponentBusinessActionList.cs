using Bfs.Core.Data;
using Bfs.Master.Data;

namespace Bfs.Master.Data.Interfaces
{
    public interface IBfsComponentBusinessActionList
    {
        Task<QueryResponse<BfsComponentBusinessActionListItem>> GetAsync(QueryRequest<BfsComponentBusinessActionListFilter> request);
    }
}