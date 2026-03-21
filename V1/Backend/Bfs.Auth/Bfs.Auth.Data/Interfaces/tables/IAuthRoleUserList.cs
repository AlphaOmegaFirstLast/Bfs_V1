using Bfs.Core.Data;
using Bfs.Auth.Data;

namespace Bfs.Auth.Data.Interfaces
{
    public interface IAuthRoleUserList
    {
        Task<QueryResponse<AuthRoleUserListItem>> GetAsync(QueryRequest<AuthRoleUserListFilter> request);
    }
}