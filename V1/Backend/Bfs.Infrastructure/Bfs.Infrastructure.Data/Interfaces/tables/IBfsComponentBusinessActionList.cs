using Bfs.Core.Data;
using Bfs.Infrastructure.Data;

namespace Bfs.Infrastructure.Data.Interfaces
{
    public interface IBfsComponentBusinessActionList
    {
        Task<QueryResponse<BfsComponentBusinessActionListItem>> GetAsync(QueryRequest<BfsComponentBusinessActionListFilter> request);
    }
}