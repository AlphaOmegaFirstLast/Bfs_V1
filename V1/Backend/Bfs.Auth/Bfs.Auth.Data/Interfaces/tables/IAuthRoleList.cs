using Bfs.Core.Data;
using Bfs.Auth.Data;

namespace Bfs.Auth.Data.Interfaces
{
    public interface IAuthRoleList
    {
        Task<QueryResponse<AuthRoleListItem>> GetAsync(QueryRequest<AuthRoleListFilter> request);
    }
}