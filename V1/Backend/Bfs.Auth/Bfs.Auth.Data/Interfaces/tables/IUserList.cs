using Bfs.Core.Data;
using Bfs.Auth.Data;

namespace Bfs.Auth.Data.Interfaces
{
    public interface IUserList
    {
        Task<QueryResponse<UserListItem>> GetAsync(QueryRequest<UserListFilter> request);
    }
}