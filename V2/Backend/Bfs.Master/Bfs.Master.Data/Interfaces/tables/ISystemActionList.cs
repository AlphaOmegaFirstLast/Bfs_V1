using Bfs.Core.Data;
using Bfs.Master.Data;

namespace Bfs.Master.Data.Interfaces
{
    public interface ISystemActionList
    {
        Task<QueryResponse<SystemActionListItem>> GetAsync(QueryRequest<SystemActionListFilter> request);
    }
}