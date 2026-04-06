using Bfs.Core.Data;
using Bfs.Auth.Data;

namespace Bfs.Auth.Data.Interfaces
{
    public interface IRoleAppList
    {
        Task<QueryResponse<RoleAppListItem>> GetAsync(QueryRequest<RoleAppListFilter> request);
    }
}

