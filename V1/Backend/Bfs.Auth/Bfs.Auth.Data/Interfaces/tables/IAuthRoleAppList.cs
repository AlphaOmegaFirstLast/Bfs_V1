using Bfs.Core.Data;
using Bfs.Auth.Data;

namespace Bfs.Auth.Data.Interfaces
{
    public interface IAuthRoleAppList
    {
        Task<QueryResponse<AuthRoleAppListItem>> GetAsync(QueryRequest<AuthRoleAppListFilter> request);
    }
}