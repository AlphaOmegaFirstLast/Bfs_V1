using Bfs.Core.Data;
using Bfs.Auth.Data;

namespace Bfs.Auth.Data.Interfaces
{
    public interface IAuthRoleComponentSystemActionList
    {
        Task<QueryResponse<AuthRoleComponentSystemActionListItem>> GetAsync(QueryRequest<AuthRoleComponentSystemActionListFilter> request);
    }
}