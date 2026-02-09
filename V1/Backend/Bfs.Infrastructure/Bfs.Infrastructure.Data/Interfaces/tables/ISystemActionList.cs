using Bfs.Core.Data;
using Bfs.Infrastructure.Data;

namespace Bfs.Infrastructure.Data.Interfaces
{
    public interface ISystemActionList
    {
        Task<QueryResponse<SystemActionListItem>> GetAsync(QueryRequest<SystemActionListFilter> request);
    }
}