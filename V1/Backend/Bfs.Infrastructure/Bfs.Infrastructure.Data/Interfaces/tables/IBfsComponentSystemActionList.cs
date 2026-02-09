using Bfs.Core.Data;
using Bfs.Infrastructure.Data;

namespace Bfs.Infrastructure.Data.Interfaces
{
    public interface IBfsComponentSystemActionList
    {
        Task<QueryResponse<BfsComponentSystemActionListItem>> GetAsync(QueryRequest<BfsComponentSystemActionListFilter> request);
    }
}