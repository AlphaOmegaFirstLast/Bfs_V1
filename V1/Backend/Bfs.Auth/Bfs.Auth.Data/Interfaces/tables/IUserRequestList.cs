using Bfs.Core.Data;
using Bfs.Auth.Data;

namespace Bfs.Auth.Data.Interfaces
{
    public interface IUserRequestList
    {
        Task<QueryResponse<UserRequestListItem>> GetAsync(QueryRequest<UserRequestListFilter> request);
    }
}

