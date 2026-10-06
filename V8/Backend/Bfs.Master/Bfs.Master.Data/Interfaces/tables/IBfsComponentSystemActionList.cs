using Bfs.Core.Data;
using Bfs.Master.Data;

namespace Bfs.Master.Data.Interfaces
{
    public interface IBfsComponentSystemActionList
    {
        Task<QueryResponse<BfsComponentSystemActionListItem>> GetAsync(QueryRequest<BfsComponentSystemActionListFilter> request);
    }
}