using Bfs.Core.Data;
using Bfs.Auth.Data;

namespace Bfs.Auth.Data.Interfaces
{
    public interface IRoleRepCompare
    {
        Task<QueryResponse<RoleRepCompareItem>> GetAsync(QueryRequest<RoleRepCompareFilter> request);
    }
}

