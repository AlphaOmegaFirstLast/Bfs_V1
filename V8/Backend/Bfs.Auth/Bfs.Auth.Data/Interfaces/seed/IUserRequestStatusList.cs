using Bfs.Core.Data;
using Bfs.Auth.Data;

namespace Bfs.Auth.Data.Interfaces
{
    public interface IUserRequestStatusList
    {
        Task<QueryResponse<UserRequestStatusListItem>> GetAsync(QueryRequest<UserRequestStatusListFilter> request);
    }
}