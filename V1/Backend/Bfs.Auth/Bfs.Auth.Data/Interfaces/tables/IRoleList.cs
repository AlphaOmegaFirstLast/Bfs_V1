using Bfs.Core.Data;
using Bfs.Auth.Data;

namespace Bfs.Auth.Data.Interfaces
{
    public interface IRoleList
    {
        Task<QueryResponse<RoleListItem>> GetAsync(QueryRequest<RoleListFilter> request);
    }
}

