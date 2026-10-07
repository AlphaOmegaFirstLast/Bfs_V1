using Bfs.Core.Data;
using Bfs.Auth.Data;

namespace Bfs.Auth.Data.Interfaces
{
    public interface IRoleComponentSystemActionList
    {
        Task<QueryResponse<RoleComponentSystemActionListItem>> GetAsync(QueryRequest<RoleComponentSystemActionListFilter> request);
    }
}